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
        public IActionResult Index(string search_name, string search_phone)
        {
            var listCustomers = dbContext.Customers.ToList();
            if (TempData["notifyMsg"] != null)
            {
                ViewBag.notifyMsg = TempData["notifyMsg"];
            }

            if (search_name != null && search_phone == null)
            {
                listCustomers = listCustomers.Where(p => p.customer_Name.Contains(search_name)).ToList();
            }

            if (search_name == null && search_phone != null)
            {
                listCustomers = listCustomers.Where(p => p.customer_Name.Contains(search_phone)).ToList();
            }

            if (search_name != null && search_phone != null)
            {
                listCustomers = listCustomers.Where(p => p.customer_Name.Contains(search_name) && p.customer_PhoneNumber == search_phone).ToList();
            }

            return View(listCustomers.ToList());
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
                if (dbContext.Customers.Where(m => m.customer_Name.Contains(newCustomer.customer_Name))
                                       .ToList()
                                       .Count() <= 0)
                {
                    dbContext.Customers.Add(newCustomer);
                    dbContext.SaveChanges();
                    TempData["notifyMsg"] = "Tạo Khách hàng mới thành công!!!";
                    return RedirectToAction("Index");
                }
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

            if (ModelState.IsValid)
            {
                Customer oldCustomer = dbContext.Customers.Find(id);

                oldCustomer.customer_Name = newCustomer.customer_Name;
                oldCustomer.customer_PhoneNumber = newCustomer.customer_PhoneNumber;
                oldCustomer.customer_Email = newCustomer.customer_Email;
                oldCustomer.customer_AddressShip = newCustomer.customer_AddressShip;

                dbContext.SaveChanges();
                TempData["notifyMsg"] = "Cập nhật thông tin KHÁCH HÀNG thành công!!!";
                return RedirectToAction("Index");
            }
            return View(newCustomer);
        }
    }
}