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

        public async Task<IActionResult> Index()
        {

            var list = await _sellerservice.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var department = await _departmentservice.FindAllAsync();
            var viewmodel = new SellerFormViewModel { Departments = department };
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var department = await _departmentservice.FindAllAsync();
                var viewmodel = new SellerFormViewModel { Seller = seller, Departments = department };

                return View(viewmodel);
            }

            await _sellerservice.InsertAsync(seller);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Provided" });
            }

            var obj = await _sellerservice.FindByIDAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Found" });
            }

            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerservice.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }


        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Provided" });
            }

            var obj = await _sellerservice.FindByIDAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Found" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Provided" });
            }

            var obj = await _sellerservice.FindByIDAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Found" });
            }
            List<Department> dep = await _departmentservice.FindAllAsync();
            SellerFormViewModel viewmodel = new SellerFormViewModel
            {
                Seller = obj,
                Departments = dep
            };

            return View(viewmodel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {

            if (ModelState.IsValid)
            {

                var department = await _departmentservice.FindAllAsync();
                var viewmodel = new SellerFormViewModel { Seller = seller, Departments = department };

                return View(viewmodel);
            }

            if (id != seller.ID)
            {
                return RedirectToAction(nameof(Error), new { message = "Id isn't match" });
            }

            try
            {
                await _sellerservice.UpdateAsync(seller);
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