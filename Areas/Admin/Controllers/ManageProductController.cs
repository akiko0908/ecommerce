using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
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
    public class ManageProductController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ManageProductController(ApplicationDbContext _context)
        {
            dbContext = _context;
        }

        [Authorize]
        public IActionResult Index(string stringSearch)
        {
            var lsProduct = dbContext.Products.Include(x => x.Brand)
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
            var product = dbContext.Products.Include(x => x.Brand)
                                            .Include(x => x.HeDieuHanh)
                                            .Include(x => x.Supplier)
                                            .Include(x => x.Categories)
                                            .Where(x => x.product_ID == id)
                                            .FirstOrDefault();

            return View("Detail", product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.listBrand = new SelectList(dbContext.Brands.ToList(), "brand_ID", "brand_Name");
            ViewBag.listSupplier = new SelectList(dbContext.Suppliers.ToList(), "supplier_ID", "supplier_Name");
            ViewBag.listCategories = new SelectList(dbContext.Categories.ToList(), "categories_ID", "categories_Name");
            ViewBag.listHeDieuHanh = new SelectList(dbContext.HeDieuHanhs.ToList(), "hdh_ID", "hdh_Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product newProduct, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product();
                if (image == null || image.Length == 0)
                {
                    product.product_Image = "";
                }
                else
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", image.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    image.CopyToAsync(stream);
                    product.product_Image = image.FileName;
                }

                product.product_Name = newProduct.product_Name;
                product.product_Price = newProduct.product_Price;
                product.product_Quantity = newProduct.product_Quantity;
                product.product_Description = newProduct.product_Description;
                product.brand_ID = newProduct.brand_ID;
                product.hdh_ID = newProduct.hdh_ID;
                product.supplier_ID = newProduct.supplier_ID;
                product.categories_ID = newProduct.categories_ID;

                dbContext.Products.Add(product);
                dbContext.SaveChanges();
                TempData["notifyMSG"] = "Đã thếm sản phẩm thành công";
                return RedirectToAction("Index");
            }
            TempData["notifyMSG"] = "Thêm sản phẩm thất bại!!!";
            return View(newProduct);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Product oldProduct = dbContext.Products.Include(p => p.Brand)
                                                   .Include(p => p.Supplier)
                                                   .Include(p => p.HeDieuHanh)
                                                   .Include(p => p.Categories)
                                                   .Where(x => x.product_ID == id)
                                                   .FirstOrDefault();
            return View(oldProduct);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id != null)
            {
                var chooseProduct = dbContext.Products.Include(p => p.Brand)
                                                      .Include(p => p.Supplier)
                                                      .Include(p => p.HeDieuHanh)
                                                      .Include(p => p.Categories)
                                                      .Where(x => x.product_ID == id)
                                                      .FirstOrDefault();

                dbContext.Products.Remove(chooseProduct);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            Product product = dbContext.Products.Include(p => p.Brand)
                                                .Include(p => p.Supplier)
                                                .Include(p => p.HeDieuHanh)
                                                .Include(p => p.Categories)
                                                .Where(x => x.product_ID == id)
                                                .FirstOrDefault();

            ViewBag.listBrand = new SelectList(dbContext.Brands.ToList(), "brand_ID", "brand_Name");
            ViewBag.listSupplier = new SelectList(dbContext.Suppliers.ToList(), "supplier_ID", "supplier_Name");
            ViewBag.listCategories = new SelectList(dbContext.Categories.ToList(), "categories_ID", "categories_Name");
            ViewBag.listHeDieuHanh = new SelectList(dbContext.HeDieuHanhs.ToList(), "hdh_ID", "hdh_Name");

            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(int? id, Product newProduct, IFormFile image)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Product oldProduct = dbContext.Products.Include(p => p.Brand)
                                                   .Include(p => p.Supplier)
                                                   .Include(p => p.HeDieuHanh)
                                                   .Include(p => p.Categories)
                                                   .Where(x => x.product_ID == id)
                                                   .FirstOrDefault();

            if (newProduct.product_ID == oldProduct.product_ID)
            {
                if (image == null || image.Length == 0)
                {
                    oldProduct.product_Image = oldProduct.product_Image;
                }
                else
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", image.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    image.CopyToAsync(stream);
                    oldProduct.product_Image = image.FileName;
                }

                oldProduct.product_Name = newProduct.product_Name;
                oldProduct.product_Price = newProduct.product_Price;
                oldProduct.product_Quantity = newProduct.product_Quantity;
                oldProduct.product_Description = newProduct.product_Description;
                oldProduct.brand_ID = newProduct.brand_ID;
                oldProduct.hdh_ID = newProduct.hdh_ID;
                oldProduct.supplier_ID = newProduct.supplier_ID;
                oldProduct.categories_ID = newProduct.categories_ID;

                dbContext.Products.Update(oldProduct);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newProduct);
        }
    }
}
