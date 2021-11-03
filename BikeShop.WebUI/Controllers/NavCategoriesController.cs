using BikeShop.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.WebUI.Controllers
{
    public class NavCategoriesController : Controller
    {
        private readonly BikeShopContext _context;
        public NavCategoriesController(BikeShopContext context) => this._context = context;
        public PartialViewResult Menu(string currentController = "Home", string category = null)
        {
            IEnumerable<Category> categories = _context.Categories.ToList();
            ViewData["Header"] = "Categories";
            ViewData["currentController"] = currentController;
            return PartialView(categories.Select(c => c.Name));
        }
        public PartialViewResult MenuCart(string returnUrl, string category = null)
        {
            IEnumerable<Category> categories = _context.Categories.ToList();
            ViewData["Header"] = "Categories";
            ViewData["ReturnUrl"] = returnUrl;
            return PartialView(categories.Select(c => c.Name));
        }
    }
}
