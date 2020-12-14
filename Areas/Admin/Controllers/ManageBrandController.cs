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
    public class ManageBrandController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ManageBrandController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            var lsBrand = dbContext.Brands;
            if (TempData["notifyMsg"] != null)
            {
                ViewBag.notifyMsg = TempData["notifyMsg"];
            }
            return View(lsBrand.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (TempData["notifyMsg"] != null)
            {
                ViewBag.notifyMsg = TempData["notifyMsg"];
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(Brand newBrand)
        {
            if (ModelState.IsValid)
            {
                dbContext.Brands.Add(newBrand);
                dbContext.SaveChanges();
                TempData["notifyMsg"] = "Thương Hiệu mới được thêm thành công!!!";
                return RedirectToAction("Index");
            }
            TempData["notifyMsg"] = "Thêm Thương hiệu mới thất bại do đã tồn tại hoặc không đúng!!!";
            return View(newBrand);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            Brand brand = dbContext.Brands.Find(id);
            if (TempData["notifyMsg"] != null)
            {
                ViewBag.notifyMsg = TempData["notifyMsg"];
            }
            return View(brand);
        }
        [HttpPost]
        public IActionResult Edit(int? id, Brand updateBrand)
        {
            if (ModelState.IsValid)
            {
                Brand oldBrand = dbContext.Brands.Find(id);

                oldBrand.brand_Name = updateBrand.brand_Name;
                dbContext.SaveChanges();
                TempData["notifyMsg"] = "Cập nhật thông tin Thương hiệu thành công!!!";
                return RedirectToAction("Index");
            }
            TempData["notifyMsg"] = "Cập nhật thông tin Thương hiệu thất bại";
            return View(updateBrand);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Brand oldBrand = dbContext.Brands.Find(id);
            return View(oldBrand);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Brand deleteBrand = dbContext.Brands.Find(id);

                dbContext.Remove(deleteBrand);
                dbContext.SaveChanges();
                TempData["notifyMsg"] = "Xóa Hãng sản xuất thành công!!!";
                return RedirectToAction("Index");
            }
            TempData["notifyMsg"] = "Xóa Thương hiệu thất bại!!!";
            return RedirectToAction("Index");
        }
    }
}