using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageProductController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ManageProductController(ApplicationDbContext _context)
        {
            dbContext = _context;
        }

        [Area("Admin")]
        public IActionResult Index()
        {
            var lsProduct = dbContext.Products
                                        .Include(x => x.Brand)
                                        .Include(x => x.HeDieuHanh)
                                        .Include(x => x.Supplier)
                                        .Include(x => x.Categories);
            if (TempData["notifyMSG"] != null)
            {
                ViewBag.notifyMsg = TempData["notifyMSG"];
            }
            return View(lsProduct.ToList());
        }

        public IActionResult Detail(int? id)
        {
            var product = dbContext.Products
                                        .Include(x => x.Brand)
                                        .Include(x => x.HeDieuHanh)
                                        .Include(x => x.Supplier)
                                        .Include(x => x.Categories)
                                        .Where(x => x.product_ID == id)
                                        .FirstOrDefault();
            return PartialView("Detail", product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult Create(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                if (newProduct != null)
                {
                    dbContext.Products.Add(newProduct);
                    dbContext.SaveChanges();
                    TempData["notifyMSG"] = "Đã thếm sản phẩm thành công";
                    return RedirectToAction("Index");
                }
            }
            return View(newProduct);
        }
    }
}
