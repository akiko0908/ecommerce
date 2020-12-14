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
    public class ManagePromotionController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ManagePromotionController(ApplicationDbContext _context)
        {
            dbContext = _context;
        }

        public IActionResult Index()
        {
            var listPromotion = dbContext.Promotions.ToList();

            if (TempData["notifyMsg"] != null)
            {
                ViewBag.notifyMsg = TempData["notifyMsg"];
            }
            return View(listPromotion);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Promotion newPromotion)
        {
            if (ModelState.IsValid)
            {
                dbContext.Promotions.Add(newPromotion);
                dbContext.SaveChanges();
                TempData["notifyMsg"] = "Tạo mới khuyến mãi thành công!!!";
                return RedirectToAction("Index");
            }
            return View(newPromotion);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                RedirectToAction("Index");
            }
            Promotion oldPromotion = dbContext.Promotions.Find(id);
            return View(oldPromotion);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Promotion oldPromotion = dbContext.Promotions.Find(id);
            dbContext.Promotions.Remove(oldPromotion);
            TempData["notifyMsg"] = "Xóa Khuyến mãi thành công!!!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Promotion oldPromotion = dbContext.Promotions.Find(id);

            return View(oldPromotion);
        }
        [HttpPost]
        public IActionResult Edit(int? id, Promotion newPromotion)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Promotion oldPromotion = dbContext.Promotions.Find(id);
            if (ModelState.IsValid)
            {
                if (oldPromotion.promotion_ID == newPromotion.promotion_ID)
                {
                    oldPromotion.promotion_Name = newPromotion.promotion_Name;
                    oldPromotion.promotion_Percent = newPromotion.promotion_Percent;

                    dbContext.SaveChanges();
                    TempData["notifyMsg"] = "Cập thông tin Khuyến mãi thành công!!!";
                    return RedirectToAction("Index");
                }
            }

            return View(newPromotion);
        }
    }
}