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
using Ecommerce.Areas.Admin.Models.ViewModel;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,
                              UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();

            if (TempData["notifyMsg"] != null)
            {
                ViewBag.notifyMsg = TempData["notifyMsg"];
            }
            return View(roles);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            if (TempData["notifyMsg"] != null)
            {
                ViewBag.notifyMsg = TempData["notifyMsg"];
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                // tạo role mới với tên role được nhập đồng thời hash idRole
                IdentityRole newRole = new IdentityRole { Name = role.RoleName };

                IdentityResult result = await _roleManager.CreateAsync(newRole);

                if (result.Succeeded)
                {
                    TempData["notifyMsg"] = "Tạo mới Role thành công!!!";
                    return RedirectToAction("Index");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            TempData["notifyMsg"] = "Thất bại do Role đã tồn tại hoặc lý do khác!!!";
            return RedirectToAction("CreateRole");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                return View(role);
            }

            return View(NotFound());
        }

        public async Task<IActionResult> ConfirmDelete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    TempData["notifyMsg"] = "Xóa Role thành công!!!";
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("DeleteRole");
        }

    }
}