using BikeShop.Domain;
using BikeShop.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeShop.WebUI.Infrastructure;

namespace BikeShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly BikeShopContext _context;

        public HomeController(BikeShopContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string category, int? page)
        {
            int pageSize = 6;
            IEnumerable<Product> products = await _context.Products.Include(p => p.Category).Include(p=>p.ImageFiles).ToListAsync();
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

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(c => c.Category)
                .Include(p => p.ImageFiles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
