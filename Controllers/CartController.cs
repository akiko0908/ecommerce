using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult CheckOut(Customer newCustomer)
        {
            //TODO: thêm chức năng nhập thông tin khách hàng khi thanh bằng PayPal
            if (dbContext.Customers.Where(m => m.customer_Name.Contains(newCustomer.customer_Name))
                                   .ToList()
                                   .Count() <= 0)
            {
                Customer customer = new Customer();
                customer = newCustomer;
                dbContext.Customers.Add(customer);

                Order order = new Order();
                order.customer_ID = dbContext.Customers.Where(m => m.customer_Name.Contains(newCustomer.customer_Name)).FirstOrDefault().customer_ID;
                order.order_CreateOnDay = DateTime.Now;
                order.order_Total = GetCartItems().Sum(m => m.orderdetail_Quantity * m.Product.product_Price);
                order.order_PaymentDate = DateTime.Now;
                order.order_PaymentMethod = "Thanh toán tiền mặt";
                order.deliverycost_ID = null;
                order.promotion_ID = null;
                foreach (var item in GetCartItems())
                {
                    dbContext.OrderDetails.Add(item);
                    item.order_ID = order.order_ID;
                }
                dbContext.Orders.Add(order);
                dbContext.SaveChanges();
                TempData["notifyMsg"] = "Đã mua hàng thành công!!!";
                return RedirectToAction("Index");
            }
            else
            {
                Customer customer = dbContext.Customers.Where(m => m.customer_Name.Contains(newCustomer.customer_Name)).FirstOrDefault();

                Order order = new Order();
                order.customer_ID = customer.customer_ID;
                order.order_CreateOnDay = DateTime.Now;
                order.order_Total = GetCartItems().Sum(m => m.orderdetail_Quantity * m.Product.product_Price);
                order.order_PaymentDate = DateTime.Now;
                order.order_PaymentMethod = "Thanh toán tiền mặt";
                order.deliverycost_ID = null;
                order.promotion_ID = null;

                foreach (var item in GetCartItems())
                {
                    dbContext.OrderDetails.Add(item);
                    item.order_ID = order.order_ID;
                }

                dbContext.Orders.Add(order);
                dbContext.SaveChanges();
                TempData["notifyMsg"] = "Đã mua hàng thành công!!!";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult InformationCash()
        {
            return View();
        }
        [HttpPost]
        public IActionResult InformationCash(Customer newCustomer)
        {
            var listCustomer = dbContext.Customers.Where(m => m.customer_Name.Contains(newCustomer.customer_Name))
                                                  .ToList();

            if (listCustomer.Count() <= 0)
            {
                dbContext.Customers.Add(newCustomer);

                Order order = new Order();
                order.customer_ID = dbContext.Customers.Where(m => m.customer_Name.Contains(newCustomer.customer_Name)).FirstOrDefault().customer_ID;
                order.order_CreateOnDay = DateTime.Now;
                order.order_Total = GetCartItems().Sum(m => m.orderdetail_Quantity * m.Product.product_Price);
                order.order_PaymentDate = DateTime.Now;
                order.order_PaymentMethod = "Thanh toán tiền mặt";
                order.deliverycost_ID = null;
                order.promotion_ID = null;

                foreach (var item in GetCartItems())
                {
                    dbContext.OrderDetails.Add(item);
                    item.order_ID = order.order_ID;
                }

                dbContext.Orders.Add(order);
                dbContext.SaveChanges();
                TempData["notifyMsg"] = "Đã mua hàng thành công!!!";
                return RedirectToAction("Index");
            }
            else
            {
                Customer customer = dbContext.Customers.Where(m => m.customer_Name.Contains(newCustomer.customer_Name)).FirstOrDefault();

                Order order = new Order();
                order.customer_ID = customer.customer_ID;
                order.order_CreateOnDay = DateTime.Now;
                order.order_Total = GetCartItems().Sum(m => m.orderdetail_Quantity * m.Product.product_Price);
                order.order_PaymentDate = DateTime.Now;
                order.order_PaymentMethod = "Thanh toán tiền mặt";
                order.deliverycost_ID = null;
                order.promotion_ID = null;

                foreach (var item in GetCartItems())
                {
                    dbContext.OrderDetails.Add(item);
                    item.order_ID = order.order_ID;
                }

                dbContext.Orders.Add(order);
                dbContext.SaveChanges();
                TempData["notifyMsg"] = "Đã mua hàng thành công!!!";
                return RedirectToAction("Index");
            }
        }

        ///
        /// yêu cầu tiến hành thanh toán bằng phương thức
        /// t   hanh toán qua cổng thanh toán PayPal
        ///
        [HttpPost]
        public async Task<IActionResult> CheckOutPayPal(double total)
        {
            var paypalAPI = new PayPalAPI(configuration);
            string url = await paypalAPI.GetRedirectURLToPayPal(total, "USD");
            return Redirect(url);
        }

        // thông báo thanh toán thành công
        public async Task<IActionResult> Success([FromQuery(Name = "paymentId")] string paymentID, [FromQuery(Name = "PayerID")] string payerID)
        {
            var paypalAPI = new PayPalAPI(configuration);
            PayPalPaymentExecuteResponse result = await paypalAPI.ExecutePayment(paymentID, payerID);

            // Debug for result of code
            ViewBag.resultSatus = result.payer.status.ToString();
            return View("Success");
        }

        // thông báo và xác nhận thanh toán thành công
        [HttpPost]
        public IActionResult ConfirmPayment(string statuspayment)
        {
            if (statuspayment == "VERIFIED")
            {
                TempData["AccessMessage"] = "Bạn đã thanh toán thành công đơn hàng!!!";
                return RedirectToAction("Index", "Cart");
            }
            return RedirectToAction("Index", "Cart");
        }

        // hủy thanh toán khi thanh toán thất bại
        public IActionResult Cancel()
        {
            return View();
        }
    }
}
