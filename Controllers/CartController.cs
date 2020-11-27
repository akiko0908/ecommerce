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
        private readonly ApplicationDbContext db;
        public IConfiguration configuration { get; }

        public CartController(ILogger<HomeController> logger, ApplicationDbContext context, IConfiguration _configuration)
        {
            _logger = logger;
            db = context;
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
        public IActionResult Index(bool isMessage)
        {
            var lsProducts = GetCartItems();
            if (isMessage == true)
                ViewBag.Message = "Add New Product To Cart Success!!!";
            return View(lsProducts);
        }

        public IActionResult AddToCart(int? idProduct)
        {
            var product = db.Products.Where(m => m.product_ID == idProduct).FirstOrDefault();

            if (product == null)
                return NotFound("Không tìm thấy sản phẩm!!!");

            // lấy danh sách sản phẩm trong session
            var cart = GetCartItems();

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
            ViewBag.Message = "Đã thêm sản phẩm thành công";
            return RedirectToAction("Index", "Cart", new { isMessage = true });
        }

        public IActionResult RemoveCart(int? productid)
        {
            var cart = GetCartItems();
            var cartItem = cart.Find(p => p.Product.product_ID == productid);

            if (cart != null)
            {
                cart.Remove(cartItem);
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

        public IActionResult CheckOut()
        {
            return View();
        }

        ///
        /// yêu cầu tiến hành thanh toán bằng phương thức
        /// thanh toán qua cổng thanh toán PayPal
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
