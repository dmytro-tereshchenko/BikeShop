using BikeShop.Domain;
using BikeShop.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.WebUI.Controllers
{
    public class NavController : Controller
    {
        private readonly BikeShopContext _context;
        public NavController(BikeShopContext context) => this._context = context;
        public PartialViewResult Menu(string currentController = "Home", string category = null)
        {
            IEnumerable<Category> categories = _context.Categories.ToList();
            ViewData["Header"] = "Categories";
            ViewData["currentController"] = currentController;
            return PartialView(categories.Select(c => c.Name));
        }
        public PartialViewResult MenuCart(Cart cart, string returnUrl, string category = null)
        {
            IEnumerable<string> categories = cart.CartItems.Select(i => i.Product.Category.Name).Distinct();
            ViewData["Header"] = "Categories";
            ViewData["ReturnUrl"] = returnUrl;
            return PartialView(categories);
        }
        public PartialViewResult MenuRoles(string currentController = "Account", string role = null)
        {
            IEnumerable<Role> roles = _context.Roles.ToList();
            ViewData["Header"] = "Roles";
            ViewData["currentController"] = currentController;
            return PartialView(roles.Select(c => c.Name));
        }
    }
}
