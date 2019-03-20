using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerservice;

        public SellersController(SellerService sellercontroller)
        {
            _sellerservice = sellercontroller;
        }

        public IActionResult Index()
        {

            var list = _sellerservice.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {

            return View();
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