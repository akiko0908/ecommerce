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
    public class ManageOperatingSystemController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ManageOperatingSystemController(ApplicationDbContext _context)
        {
            dbContext = _context;
        }

        public IActionResult Index()
        {
            var listOS = dbContext.HeDieuHanhs.ToList();
            if (TempData["notifyMsg"] != null)
            {
                ViewBag.notifyMsg = TempData["notifyMsg"];
            }
            return View(listOS);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(HeDieuHanh newOS)
        {
            if (ModelState.IsValid)
            {
                dbContext.HeDieuHanhs.Add(newOS);
                dbContext.SaveChanges();
                TempData["notifyMsg"] = "Thêm HỆ ĐIỀU HÀNH mới thành công!!!";
                return RedirectToAction("Index");
            }
            return View(newOS);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            HeDieuHanh chooseOS = dbContext.HeDieuHanhs.Find(id);
            return View(chooseOS);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            HeDieuHanh chooseOS = dbContext.HeDieuHanhs.Find(id);
            dbContext.HeDieuHanhs.Remove(chooseOS);
            dbContext.SaveChanges();
            TempData["notifyMsg"] = "Xóa HỆ ĐIỀU HÀNH của sản phẩm thành công!!!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            HeDieuHanh chooseOS = dbContext.HeDieuHanhs.Find(id);
            return View(chooseOS);
        }
        [HttpPost]
        public IActionResult Edit(int? id, HeDieuHanh newOS)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            HeDieuHanh oldOS = dbContext.HeDieuHanhs.Find(id);
            if (ModelState.IsValid)
            {
                if (oldOS.hdh_ID == newOS.hdh_ID)
                {
                    oldOS.hdh_Name = newOS.hdh_Name;
                    dbContext.SaveChanges();
                    TempData["notifyMsg"] = "Cập nhật thông tin HỆ ĐIỀU HÀNH thành công!!!";
                    return RedirectToAction("Index");
                }
            }
            return View(newOS);
        }
    }
}
