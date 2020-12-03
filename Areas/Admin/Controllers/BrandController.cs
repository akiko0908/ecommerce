using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Brand"), Authorize(Roles = "Admin")]
    public class BrandController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public BrandController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            var lsBrand = dbContext.Brands;
            if (TempData["SuccessDelete"] != null)
            {
                ViewBag.DeleteMsg = TempData["SuccessDelete"];
            }
            return View(lsBrand.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView();
        }
        public IActionResult Create(Brand newBrand)
        {
            if (ModelState.IsValid)
            {
                dbContext.Brands.Add(newBrand);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            Brand brand = dbContext.Brands.Find(id);
            return PartialView(brand);
        }

        [HttpPost]
        public IActionResult Edit(int? id, string nameBrandChange)
        {
            Brand oldBrand = dbContext.Brands.Find(id);
            if (oldBrand.brand_Name.CompareTo(nameBrandChange) == 0)
            {
                oldBrand.brand_Name = nameBrandChange;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", "Brand", oldBrand);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Brand oldBrand = dbContext.Brands.Find(id);
            return PartialView(oldBrand);
        }

        [HttpPost]
        public IActionResult Delete(Brand oldBrand)
        {
            if (ModelState.IsValid)
            {
                if (oldBrand != null)
                {
                    dbContext.Remove(oldBrand);
                    dbContext.SaveChanges();
                    TempData["SuccessDelete"] = "Xóa Hãng sản xuất thành công!!!";
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Details(int? id)
        {
            Brand chooseBrand = dbContext.Brands.Find(id);
            return PartialView(chooseBrand);
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            Brand chooseBrand = dbContext.Brands.Find(id);
            return PartialView(chooseBrand);
        }

        [HttpPost]
        public IActionResult Update(Brand chooseBrand)
        {
            if (ModelState.IsValid)
            {
                if (chooseBrand != null)
                {
                    dbContext.Brands.Remove(chooseBrand);
                    dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return PartialView("Update", chooseBrand);
        }
    }
}