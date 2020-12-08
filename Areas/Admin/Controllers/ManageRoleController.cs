using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageRoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public ManageRoleController(RoleManager<IdentityRole> _roleManager)
        {
            roleManager = _roleManager;
        }

        // TODO: cần hoàn thành các chức năng quản lý Role truy cập
        public IActionResult Index()
        {
            var listRole = roleManager.Roles.ToList();
            return View(listRole);
        }

        // create new role for webapp
        public IActionResult CreateRole()
        {
            return View(new IdentityRole());
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(IdentityRole newRole)
        {
            await roleManager.CreateAsync(newRole);
            return RedirectToAction("Index");
        }
    }
}