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
    public class ManageCustomerController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ManageCustomerController(ApplicationDbContext _context)
        {
            dbContext = _context;
        }

        // TODO: hoàn chỉnh các chức quản lý thông tin
        public IActionResult Index()
        {
            var listCustomers = dbContext.Customers.ToList();
            if (TempData["notifyMsg"] != null)
            {
                ViewBag.notifyMsg = TempData["notifyMsg"];
            }
            return View(listCustomers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer newCustomer)
        {
            if (ModelState.IsValid)
            {
                dbContext.Customers.Add(newCustomer);
                dbContext.SaveChanges();
                TempData["notifyMsg"] = "Tạo Khách hàng mới thành công!!!";
                return RedirectToAction("Index");
            }
            return View(newCustomer);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Customer chooseCustomer = dbContext.Customers.Find(id);
            return View(chooseCustomer);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Customer chooseCustomer = dbContext.Customers.Find(id);
            dbContext.Customers.Remove(chooseCustomer);
            dbContext.SaveChanges();
            TempData["notifyMsg"] = "Xóa thông tin Khách hàng thành công!!!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Customer chooseCustomer = dbContext.Customers.Find(id);
            return View(chooseCustomer);
        }
        [HttpPost]
        public IActionResult Edit(int? id, Customer newCustomer)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                Customer oldCustomer = dbContext.Customers.Find(id);

                oldCustomer.customer_Name = newCustomer.customer_Name;
                oldCustomer.customer_PhoneNumber = newCustomer.customer_PhoneNumber;
                oldCustomer.customer_Email = newCustomer.customer_Email;
                oldCustomer.customer_AddressShip1 = newCustomer.customer_AddressShip1;
                oldCustomer.customer_AddressShip2 = newCustomer.customer_AddressShip2;

                dbContext.SaveChanges();
                TempData["notifyMsg"] = "Cập nhật thông tin KHÁCH HÀNG thành công!!!";
                return RedirectToAction("Index");
            }
            return View(newCustomer);
        }
    }
}