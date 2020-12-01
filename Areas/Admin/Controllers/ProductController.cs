using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ProductController(ApplicationDbContext _context)
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
            return View(lsProduct.ToList());
        }
    }
}
