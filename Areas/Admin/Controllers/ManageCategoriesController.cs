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
    public class ManageCategoriesController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ManageCategoriesController(ApplicationDbContext _context)
        {
            dbContext = _context;
        }

        public IActionResult Index()
        {
            var listCategories = dbContext.Categories.ToList();
            if (TempData["notifyMsg"] != null)
            {
                ViewBag.notifyMsg = TempData["notifyMsg"];
            }
            return View(listCategories);
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
        public IActionResult Create(Categories newCategories)
        {
            if (ModelState.IsValid)
            {
                dbContext.Categories.Add(newCategories);
                dbContext.SaveChanges();
                TempData["notifyMsg"] = "Thêm mới loại sản phẩm thành công!!!";
                return RedirectToAction("Index");
            }
            TempData["notifyMsg"] = "Thêm mới loại sản phẩm thất bại!!!";
            return View(newCategories);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Categories chooseCategories = dbContext.Categories.Find(id);
            return View(chooseCategories);
        }
        [HttpPost]
        public IActionResult Edit(int? id, Categories newCategories)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Categories oldCategprries = dbContext.Categories.Find(id);
                if (oldCategprries.categories_ID == newCategories.categories_ID
                    && oldCategprries.categories_Name != newCategories.categories_Name)
                {
                    oldCategprries.categories_Name = newCategories.categories_Name;
                    dbContext.SaveChanges();
                    TempData["notifyMsg"] = "Cập nhật thông tin Loại sản phẩm thành công!!!";
                    return RedirectToAction("Index");
                }
            }
            return View(newCategories);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Categories chooseCategories = dbContext.Categories.Find(id);
            return View(chooseCategories);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Categories chooseCategories = dbContext.Categories.Find(id);

            dbContext.Categories.Remove(chooseCategories);
            dbContext.SaveChanges();
            TempData["notifyMsg"] = "Loại sản phẩm được xóa thành công";
            return RedirectToAction("Index");
        }
    }
}