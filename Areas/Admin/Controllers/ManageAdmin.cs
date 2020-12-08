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
    public class ManageAdminController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly RoleManager<IdentityRole> roleManager;

        public ManageAdminController(ApplicationDbContext _context)
        {
            dbContext = _context;
        }

        // TODO: cần hoàn thành các chức năng quản lý tài khoản dưới quyền truy cập là Administrator
        public IActionResult Index()
        {
            var listUser = dbContext.Users;
            return View();
        }

        // TODO: hoàn chỉnh các chức năng quản lý cho Account Administator
    }
}
