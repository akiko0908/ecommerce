using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Ecommerce.Models;
using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Ecommerce.PayPal;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext dbContext;
        public IConfiguration configuration { get; }

        public CartController(ILogger<HomeController> logger, ApplicationDbContext context, IConfiguration _configuration)
        {
            _logger = logger;
            dbContext = context;
            configuration = _configuration;
        }

        // key lưu chuỗi Json của Cart
        public const string CartKey = "cart";

        // lấy cart từ session (danh sách CartItem)
        List<OrderDetail> GetCartItems()
        {
            var session = HttpContext.Session;

            // lấy thông tin từ session
            string jsonCart = session.GetString(CartKey);

            if (jsonCart != null)
            {
                return JsonConvert.DeserializeObject<List<OrderDetail>>(jsonCart);
            }
            return new List<OrderDetail>();
        }

        // Xóa cart khỏi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CartKey);
        }

        // Lưu Cart (Danh sách orderdetails) vào session
        void SaveCartSession(List<OrderDetail> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CartKey, jsoncart);
        }

        // hiển thị danh sách sản phẩm giỏ hàng
        public IActionResult Index()
        {
            var lsProducts = GetCartItems();
            if (TempData["RemoveMessage"] != null)
            {
                ViewBag.RemoveMsg = TempData["RemoveMessage"];
            }
            return View(lsProducts);
        }

        public IActionResult AddToCart(int? idProduct)
        {
            var product = dbContext.Products.Where(m => m.product_ID == idProduct).FirstOrDefault();

            if (product == null)
                return NotFound("Không tìm thấy sản phẩm!!!");

            // lấy danh sách sản phẩm trong session
            var cart = GetCartItems();

            // tìm sản phẩm đã thêm vào cart trước đó
            // kiểm tra đã tồn tại thì tăng số lượng
            // chưa tồn tài thì thêm mới vào cart
            var cartItem = cart.Find(m => m.Product.product_ID == idProduct);
            if (cartItem != null)
            {
                cartItem.orderdetail_Quantity++;
            }
            else
            {
                cart.Add(new OrderDetail() { Product = product, orderdetail_Quantity = 1 });
            }

            SaveCartSession(cart);
            TempData["SuccessMessage"] = "Đã thêm sản phẩm vào giỏ hàng thành công!!!";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult RemoveCart(int? productid)
        {
            var cart = GetCartItems();
            var cartItem = cart.Find(p => p.Product.product_ID == productid);

            if (cart != null)
            {
                cart.Remove(cartItem);
                TempData["RemoveMessage"] = "Đã xóa sản phẩm khỏi giỏ hàng!!!";
                SaveCartSession(cart);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // [HttpPost]
        // public IActionResult UpdateCart([FromForm] int productid, [FromForm] int orderdetail_Quantity)
        // {
        //     // Cập nhật Cart thay đổi số lượng quantity ...
        //     var cart = GetCartItems();

        //     var cartitem = cart.Find(p => p.Product.product_ID == productid);
        //     if (cartitem != null)
        //     {
        //         // Đã tồn tại, tăng thêm 1
        //         cartitem.orderdetail_Quantity = orderdetail_Quantity;
        //     }

        //     SaveCartSession(cart);
        //     // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
        //     return Ok();
        // }

        // thanh toán tiền mặt hơặc tại quầy
        // hoặc thanh toán sau khi customer nhận hàng
        [HttpGet]
        public IActionResult CheckOut()
        {
            ViewBag.listCart = GetCartItems();
            ViewBag.listDeliveryCost = new SelectList(dbContext.DeliveryCosts.ToList(),
                                                      "deliverycost_ID",
                                                      "deliverycost_AreaName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CheckOut(Customer newCustomer,
                                                  string option_payment,
                                                  string promotion_code,
                                                  string deliverycost_id)
        {
            ViewBag.listCart = GetCartItems();
            ViewBag.listDeliveryCost = new SelectList(dbContext.DeliveryCosts.ToList(),
                                                      "deliverycost_ID",
                                                      "deliverycost_AreaName");

            if (ModelState.IsValid)
            {
                // kiểm tra khách hàng đã tồn tại chưa
                Customer customer = new Customer();
                if (dbContext.Customers.Where(p => p.customer_Name.Contains(newCustomer.customer_Name))
                                       .ToList()
                                       .Count() <= 0)
                {
                    // tạo khách hàng mới
                    customer.customer_Name = newCustomer.customer_Name;
                    customer.customer_PhoneNumber = newCustomer.customer_PhoneNumber;
                    customer.customer_Email = newCustomer.customer_Email;
                    customer.customer_AddressShip = newCustomer.customer_AddressShip;

                    dbContext.Customers.Add(customer);
                    dbContext.SaveChanges();
                }
                else
                {
                    customer = dbContext.Customers.Where(p => p.customer_Name.Contains(newCustomer.customer_Name)
                                                              && p.customer_PhoneNumber == newCustomer.customer_PhoneNumber).FirstOrDefault();
                }

                // tạo đơn hàng cho khách hàng
                Order order = new Order();
                order.order_CreateOnDay = DateTime.Now.Date;
                order.order_Total = GetCartItems().Sum(p => p.orderdetail_Quantity * p.Product.product_Price);
                order.order_PaymentDate = null;
                if (option_payment == "cod")
                {
                    order.order_PaymentMethod = "Thanh toán COD";
                    order.StatusOrder = "Chưa xác nhận đơn";
                }
                else if (option_payment == "paypal")
                {
                    order.order_PaymentMethod = "Thanh toán PayPal";
                    order.StatusOrder = "Đã xác nhận đơn";
                }
                order.customer_ID = customer.customer_ID;
                if (deliverycost_id == "0")
                    order.deliverycost_ID = 1;
                else
                    order.deliverycost_ID = Convert.ToInt32(deliverycost_id);

                if (string.IsNullOrEmpty(promotion_code))
                    order.promotion_ID = 1;
                else
                    order.promotion_ID = Convert.ToInt32(promotion_code);

                dbContext.Orders.Add(order);
                dbContext.SaveChanges();
                foreach (var item in GetCartItems())
                {
                    item.order_ID = order.order_ID;
                    dbContext.OrderDetails.Add(item);
                    Product productUpdate = dbContext.Products.Find(item.product_ID);
                    productUpdate.product_Quantity -= item.orderdetail_Quantity;
                    dbContext.Products.Update(productUpdate);
                    dbContext.SaveChanges();
                }

                if (option_payment == "cod")
                {
                    ClearCart();
                    return RedirectToAction("CheckOutCODSuccess");
                }
                else if (option_payment == "paypal")
                {
                    int id_order = order.order_ID;
                    return await CheckOutPayPal(id_order);
                }
            }

            return View();
        }

        public IActionResult CheckOutCODSuccess()
        {
            ViewData["status_payment"] = "success";
            return View();
        }

        ///
        /// yêu cầu tiến hành thanh toán bằng phương thức
        /// thanh toán qua cổng thanh toán PayPal
        ///
        [HttpPost]
        public async Task<IActionResult> CheckOutPayPal(int id_order)
        {
            Order order = dbContext.Orders.Include(p => p.Promotion)
                                          .Include(p => p.DeliveryCost)
                                          .Where(p => p.order_ID == id_order)
                                          .FirstOrDefault();
            double total_order = Convert.ToDouble(order.order_Total
                                                  - (order.Promotion.promotion_Percent * order.order_Total)
                                                  + order.DeliveryCost.deliverycost_Cost);

            TempData["order_id"] = id_order;

            var paypalAPI = new PayPalAPI(configuration);
            string url = await paypalAPI.GetRedirectURLToPayPal(total_order, "USD");
            return Redirect(url);
        }

        // thông báo thanh toán thành công
        public async Task<IActionResult> Success([FromQuery(Name = "paymentId")] string paymentID, [FromQuery(Name = "PayerID")] string payerID)
        {
            var paypalAPI = new PayPalAPI(configuration);
            PayPalPaymentExecuteResponse result = await paypalAPI.ExecutePayment(paymentID, payerID);

            if (result.payer.status == "VERIFIED")
            {
                var order_id = TempData["order_id"];
                Order order = dbContext.Orders.Find(order_id);
                order.StatusOrder = "Đã thanh toán";
                order.order_PaymentDate = DateTime.Now.Date;
                dbContext.Orders.Update(order);
                dbContext.SaveChanges();
                ClearCart();
            }

            // Debug for result of code
            ViewBag.resultSatus = result.payer.status.ToString();
            return View("Success");
        }

        // hủy thanh toán khi thanh toán thất bại
        public IActionResult Cancel()
        {
            return View();
        }
    }
}
