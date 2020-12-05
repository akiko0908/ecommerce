using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data;

namespace Ecommerce.Controllers
{
    public class AccountController : Controller
    {
        // TODO: hoàn thành trang login, logout và register
        private readonly ApplicationDbContext dbContext;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(ApplicationDbContext _context, RoleManager<IdentityRole> _roleManager)
        {
            dbContext = _context;
            roleManager = _roleManager;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult LoginAdmin()
        {
            return View();
        }
    }
}
