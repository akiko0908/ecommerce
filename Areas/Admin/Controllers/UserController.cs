using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;

        public UserController(ApplicationDbContext _context,
                              RoleManager<IdentityRole> _roleManager,
                              UserManager<AppUser> _userManager)
        {
            dbContext = _context;
            roleManager = _roleManager;
            userManager = _userManager;
        }

        // TODO: cần hoàn thành các chức năng quản lý tài khoản dưới quyền truy cập là Administrator
        public IActionResult Index()
        {
            var listUser = userManager.GetUsersInRoleAsync("Administrator").ToString();
            return View(listUser);
        }

        // TODO: hoàn chỉnh các chức năng quản lý cho Account Administator
    }
}
