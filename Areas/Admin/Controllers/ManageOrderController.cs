using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageOrderController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ManageOrderController(ApplicationDbContext _context)
        {
            dbContext = _context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var listOrder = dbContext.Orders.Include(p => p.Customer)
                                            .Include(p => p.DeliveryCost)
                                            .Include(p => p.Promotion)
                                            .Include(p => p.OrderDetails);
            return View(listOrder.ToList());
        }

        [HttpGet]
        public IActionResult CancelOrder(int? id)
        {
            var order = dbContext.Orders.Include(m => m.Customer)
                                        .Include(m => m.DeliveryCost)
                                        .Include(m => m.Promotion)
                                        .Include(m => m.OrderDetails)
                                        .Where(m => m.order_ID == id)
                                        .FirstOrDefault();
            ViewBag.listOrderDetails = dbContext.OrderDetails.Include(p => p.Product)
                                                             .Where(p => p.order_ID == order.order_ID)
                                                             .ToList();
            return View(order);
        }

        public IActionResult ConfirmCancel(int? id)
        {
            var orderSelect = dbContext.Orders.Include(p => p.OrderDetails).Where(m => m.order_ID == id).FirstOrDefault();

            List<OrderDetail> lsOrderDetails = dbContext.OrderDetails.Include(p => p.Product)
                                                       .Where(p => p.order_ID == orderSelect.order_ID)
                                                       .ToList();

            foreach (var item in lsOrderDetails)
            {
                Product product = item.Product;
                product.product_Quantity = product.product_Quantity + item.orderdetail_Quantity;
                dbContext.SaveChanges();
            }

            orderSelect.StatusOrder = "Đã hủy đơn";
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult GetOrderDetails(int? orderID)
        {
            var listOrderDetails = dbContext.OrderDetails.Include(m => m.Order).Where(m => m.Order.order_ID == orderID).ToList();
            return View(listOrderDetails);
        }

        public IActionResult UpdateStatus(int? id, string statusOrder)
        {
            if (id != null && statusOrder != null)
            {
                Order order = dbContext.Orders.Find(id);
                order.StatusOrder = statusOrder;
                if (statusOrder == "Đã thanh toán")
                {
                    order.order_PaymentDate = DateTime.Now;
                }
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            return NotFound("Không tìm thấy đối tượng!!!");
        }
    }
}