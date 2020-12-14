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
    public class ManageSupplierController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ManageSupplierController(ApplicationDbContext _context)
        {
            dbContext = _context;
        }

        public IActionResult Index()
        {
            var listSupplier = dbContext.Suppliers.ToList();
            if (TempData["notifyMsg"] != null)
            {
                ViewBag.notifyMsg = TempData["notifyMsg"];
            }
            return View(listSupplier);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Supplier newSupplier)
        {
            if (ModelState.IsValid)
            {
                dbContext.Suppliers.Add(newSupplier);
                dbContext.SaveChanges();
                TempData["notifyMsg"] = "Nhà cung cấp được thêm thành công!!!";
                return RedirectToAction("Index");
            }
            return View(newSupplier);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Supplier oldSupplier = dbContext.Suppliers.Find(id);
            return View(oldSupplier);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Supplier oldSupplier = dbContext.Suppliers.Find(id);

            dbContext.Suppliers.Remove(oldSupplier);
            dbContext.SaveChanges();
            TempData["notifyMsg"] = "Xóa Nhà cung cấp thành công!!!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Supplier oldSupplier = dbContext.Suppliers.Find(id);
            return View(oldSupplier);
        }
        [HttpPost]
        public IActionResult Edit(int? id, Supplier newSupplier)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Supplier oldSupplier = dbContext.Suppliers.Find(id);
            if (ModelState.IsValid)
            {
                if (oldSupplier.supplier_ID == newSupplier.supplier_ID)
                {
                    oldSupplier.supplier_Name = newSupplier.supplier_Name;

                    dbContext.SaveChanges();
                    TempData["notifyMsg"] = "Cập nhật thông tin Nhà cung cấp thành công!!!";
                    return RedirectToAction("Index");
                }
            }
            return View(newSupplier);
        }
    }
}