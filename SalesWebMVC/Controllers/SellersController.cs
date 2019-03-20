using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerservice;
        private readonly DepartmentService _departmentservice;

        public SellersController(SellerService sellercontroller, DepartmentService departmentservice)
        {
            _sellerservice = sellercontroller;
            _departmentservice = departmentservice;
        }

        public IActionResult Index()
        {

            var list = _sellerservice.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var department = _departmentservice.FindAll();
            var viewmodel = new SellerFormViewModel { Departments = department };
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {

            _sellerservice.Insert(seller);

            return RedirectToAction(nameof(Index));
        }
    }
}