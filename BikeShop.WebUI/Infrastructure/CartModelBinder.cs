using BikeShop.WebUI.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.WebUI.Infrastructure
{
    public class CartModelBinder : IModelBinder

    {
        private readonly IModelBinder fallbackBinder;
        public CartModelBinder(IModelBinder fallbackBinder)
        {
            this.fallbackBinder = fallbackBinder;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            const string sessionKey = "Cart";
            Cart cart = null;
            if (bindingContext.HttpContext.Session != null && bindingContext.HttpContext.Session.Keys.Contains(sessionKey))
            {
                cart = bindingContext.HttpContext.Session.Get<Cart>(sessionKey);
            }
            if (cart == null)
            {
                cart = new Cart();
                if (bindingContext.HttpContext.Session != null)
                {
                    bindingContext.HttpContext.Session.Set<Cart>(sessionKey, cart);
                }
            }
            bindingContext.Result = ModelBindingResult.Success(cart);
            return Task.CompletedTask;
        }
    }
}
