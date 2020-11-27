using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ecommerce.Models;
using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext db;

        public ProductController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            db = context;
        }

        //lấy danh sách sản phẩm hiện có liệt kê hiển thị ra màn hình
        public IActionResult Index()
        {
            var lsProduct = db.Products;
            return View(lsProduct.ToList());
        }

        // hiển thị trang thêm sản phẩm
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // lấy thông tin sản phẩm đã thêm xử lý lưu vào database
        [HttpPost]
        public IActionResult Create(Product newProduct)
        {
            Product product = new Product();

            if (!ModelState.IsValid)
            {
                if (newProduct != null)
                {
                    product = newProduct;
                    return RedirectToAction("Index");
                }
            }
            return View(newProduct);
        }

    }
}
