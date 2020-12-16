using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Ecommerce.Models;
using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext db;

        public ProductController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            db = context;
        }

        public IActionResult Detail(int? id)
        {
            Product product = db.Products.Include(m => m.Brand)
                                         .Include(m => m.Categories)
                                         .Include(m => m.HeDieuHanh)
                                         .Include(m => m.Supplier)
                                         .Where(p => p.product_ID == id).FirstOrDefault();
            return View(product);
        }
    }
}
