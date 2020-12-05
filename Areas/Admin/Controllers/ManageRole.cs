using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageRole : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public ManageRole(RoleManager<IdentityRole> _roleManager)
        {
            roleManager = _roleManager;
        }

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