using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data;
using Ecommerce.Models;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageDeliveryCostController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ManageDeliveryCostController(ApplicationDbContext _context)
        {
            dbContext = _context;
        }

        public IActionResult Index()
        {
            var listDeliveryCost = dbContext.DeliveryCosts.ToList();
            if (TempData["notifyMsg"] != null)
            {
                ViewBag.notifyMsg = TempData["notifyMsg"];
            }
            return View(listDeliveryCost);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DeliveryCost newDeliveryCost)
        {
            if (ModelState.IsValid)
            {
                dbContext.DeliveryCosts.Add(newDeliveryCost);
                dbContext.SaveChanges();
                TempData["notifyMsg"] = "Thêm KHU VỰC GIAO HÀNG thành công!!!";
                return RedirectToAction("Index");
            }
            return View(newDeliveryCost);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            DeliveryCost chooseDeliveryCost = dbContext.DeliveryCosts.Find(id);
            return View(chooseDeliveryCost);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            DeliveryCost chooseDeliveryCost = dbContext.DeliveryCosts.Find(id);
            dbContext.DeliveryCosts.Remove(chooseDeliveryCost);
            dbContext.SaveChanges();
            TempData["notifyMsg"] = "Xóa KHU VỰC GIAO HÀNG thành công!!!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            DeliveryCost chooseDeliveryCost = dbContext.DeliveryCosts.Find(id);
            return View(chooseDeliveryCost);
        }
        [HttpPost]
        public IActionResult Edit(int? id, DeliveryCost newDeliveryCost)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            DeliveryCost oldDeliveryCost = dbContext.DeliveryCosts.Find(id);
            if (ModelState.IsValid)
            {
                if (oldDeliveryCost.deliverycost_ID == id)
                {
                    oldDeliveryCost.deliverycost_AreaName = newDeliveryCost.deliverycost_AreaName;
                    oldDeliveryCost.deliverycost_Cost = newDeliveryCost.deliverycost_Cost;

                    dbContext.SaveChanges();
                    TempData["notifyMsg"] = "Cập nhật THÔNG TIN DỊCH VỤ GIAO HÀNG thành công!!!";
                    return RedirectToAction("Index");
                }
            }
            return View(newDeliveryCost);
        }
    }
}