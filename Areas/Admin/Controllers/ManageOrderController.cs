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
    [Authorize]
    public class ManageOrderController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ManageOrderController(ApplicationDbContext _context)
        {
            dbContext = _context;
        }

        public IActionResult Index()
        {
            var listOrder = dbContext.Orders.Include(p => p.Customer)
                                            .Include(p => p.DeliveryCost)
                                            .Include(p => p.Promotion)
                                            .Include(p => p.OrderDetails);
            return View(listOrder.ToList());
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var order = dbContext.Orders.Include(m => m.Customer)
                                            .Include(m => m.DeliveryCost)
                                            .Include(m => m.Promotion)
                                            .Include(m => m.OrderDetails)
                                            .Where(m => m.order_ID == id);

            return View(order);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int? id, Order chooseOrder)
        {
            var orderSelect = dbContext.Orders.Where(m => m.order_ID == id).FirstOrDefault();
            dbContext.Orders.Remove(orderSelect);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult GetOrderDetails(int? orderID)
        {
            var listOrderDetails = dbContext.OrderDetails.Include(m => m.Order).Where(m => m.Order.order_ID == orderID).ToList();
            return View(listOrderDetails);
        }

        public IActionResult Edit(int? id, string statusOrder)
        {
            if (id != null && statusOrder != null)
            {
                Order order = dbContext.Orders.Find(id);
                order.StatusOrder = statusOrder;
                if (statusOrder == "Đã thanh toán")
                {
                    order.order_PaymentDate = DateTime.Now;
                }
                dbContext.Orders.Update(order);
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            return NotFound("Không tìm thấy đối tượng!!!");
        }
    }
}