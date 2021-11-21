using BikeShop.Domain;
using BikeShop.WebUI.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connStr = Configuration.GetConnectionString("SqlConnStr");
            services.AddDbContext<BikeShopContext>(options => options.UseSqlServer(connStr));
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/login");
                });
            services.AddControllersWithViews();
            services.AddControllersWithViews(options =>
            options.ModelBinderProviders.Insert(0, new CartModelBinderProvider()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "root",
                    pattern: "",
                    defaults: new { controller = "Home", action = "Index", category = (string)null, page = 1 });
                endpoints.MapControllerRoute(
                    name: "page",
                    pattern: "page{page}",
                    defaults: new { controller = "Home", action = "Index", category = (string)null },
                    constraints: new { page = @"\d+" });
                endpoints.MapControllerRoute(
                    name: "categories",
                    pattern: "{category}",
                    defaults: new { controller = "Home", action = "Index", page = 1 });
                endpoints.MapControllerRoute(
                    name: "standart",
                    pattern: "{category}/page{page}",
                    defaults: new { controller = "Home", action = "Index", category = (string)null },
                    constraints: new { page = @"\d+" });
                endpoints.MapControllerRoute(
                   name: "login",
                   pattern: "login",
                   defaults: new { controller = "Account", action = "Login" });
                endpoints.MapControllerRoute(
                   name: "register",
                   pattern: "register",
                   defaults: new { controller = "Account", action = "Register" });
                endpoints.MapControllerRoute(
                   name: "logout",
                   pattern: "logout",
                   defaults: new { controller = "Account", action = "Logout" });
                endpoints.MapControllerRoute(
                    name: "adminPanel",
                    pattern: "admin/{controller:regex(^P.*|^Cat.*|^A.*|^R.*)=Product}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
