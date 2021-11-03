using BikeShop.WebUI.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.WebUI.Infrastructure
{
    public class CartModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            ILoggerFactory loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();
            IModelBinder modelBinder = new CartModelBinder(new SimpleTypeModelBinder(typeof(Cart), loggerFactory));
            return context.Metadata.ModelType == typeof(Cart) ? modelBinder : null;
        }
    }
}
