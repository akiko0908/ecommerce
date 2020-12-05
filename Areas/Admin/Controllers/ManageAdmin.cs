using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class ManageAdminController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ManageAdminController(ApplicationDbContext _context)
        {
            dbContext = _context;
        }

        public IActionResult Index()
        {
            var listUser = dbContext.Users;
            return View();
        }

    }
}
