using BikeShop.Domain;
using BikeShop.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeShop.WebUI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BikeShop.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly BikeShopContext _context;

        public CartController(BikeShopContext context)
        {
            _context = context;
        }
        public async Task<RedirectToActionResult> AddToCart(Cart cart, int id, string returnUrl)
        {
            Product product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(c => c.Id == id);
            if (product != null)
            {
                cart.AddItem(product, 1);
                HttpContext.Session.Set<Cart>("Cart", cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public async Task<RedirectToActionResult> RempoveFromCart(Cart cart, int id, string returnUrl)
        {
            Product product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(c => c.Id == id);
            if (product != null)
            {
                cart.RemoveItem(product);
                HttpContext.Session.Set<Cart>("Cart", cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public ActionResult Index(Cart cart, string returnUrl, string category = null)
        {
            cart.CurrentCategory = category;
            CartIndexViewModel viewModel = new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl };
            return View(viewModel);
        }

    }
}
