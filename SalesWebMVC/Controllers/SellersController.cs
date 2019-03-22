using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;

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
            if (!ModelState.IsValid)
            {
                var department = _departmentservice.FindAll();
                var viewmodel = new SellerFormViewModel { Seller = seller, Departments = department };

                return View(viewmodel);
            }

            _sellerservice.Insert(seller);

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Provided" });
            }

            var obj = _sellerservice.FindByID(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Found" });
            }

            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerservice.Remove(id);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Details(int? id)
        {

            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Provided" });
            }

            var obj = _sellerservice.FindByID(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Found" });
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Provided" });
            }

            var obj = _sellerservice.FindByID(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Found" });
            }
            List<Department> dep = _departmentservice.FindAll();
            SellerFormViewModel viewmodel = new SellerFormViewModel
            {
                Seller = obj,
                Departments = dep
            };

            return View(viewmodel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {

            if (ModelState.IsValid)
            {

                var department = _departmentservice.FindAll();
                var viewmodel = new SellerFormViewModel { Seller = seller, Departments = department };

                return View(viewmodel);
            }

            if (id != seller.ID)
            {
                return RedirectToAction(nameof(Error), new { message = "Id isn't match" });
            }

            try
            {
                _sellerservice.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewmodel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewmodel);

        }
    }
}