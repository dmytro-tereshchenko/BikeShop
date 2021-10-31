using BikeShop.Domain;
using BikeShop.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BikeShopContext _context;

        public HomeController(ILogger<HomeController> logger, BikeShopContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(string category, int? page)
        {
            /*int pageSize = 2;
            var products = _context.Products.AsQueryable();
            var categories = products.Select(t => t.Category)
            .Distinct()
            .OrderBy(t => t.Id);
            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(t => t.Category.Name.ToLower() == category.ToLower());
            }
            int pageNumber = page ?? 1;
            return View(new HomeIndexViewModel { Products = products.ToPagedList(pageNumber, pageSize), CurrentCategory = category });*/

            int pageSize = 3;
            IEnumerable<Product> products = await _context.Products.ToListAsync();
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page ?? 1,
                PageSize = pageSize,
                TotalItems = category == null ? products.Count() :
                products.Where(g => g.Category.Name == category).Count()
            };
            IEnumerable<Product> productsResult = category == null ? products.
                OrderBy(g => g.Id).
                Skip(((page ?? 1) - 1) * pageSize).Take(pageSize)
                : products.
                Where(g => g.Category.Name.ToLower() == category.ToLower()).
                OrderBy(g => g.Id).
                Skip(((page ?? 1) - 1) * pageSize).Take(pageSize);
            HomeIndexViewModel viewModel = new HomeIndexViewModel
            {
                Products = productsResult,
                PageInfo = pageInfo,
                CurrentCategory = category
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
