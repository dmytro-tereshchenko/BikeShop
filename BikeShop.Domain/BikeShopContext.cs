using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikeShop.Domain
{
    public class BikeShopContext:DbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Category> Categories { get; set; }
        public BikeShopContext(DbContextOptions<BikeShopContext> options) :base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Category> categories = new List<Category>()
            {
                new Category(){ Id=1, Name="Bicycles" },
                new Category(){ Id=2, Name="Cycling clothes" },
                new Category(){ Id=3, Name="Bicycle shoes" }
            };
            modelBuilder.Entity<Category>().HasData(categories);
            List<Product> products = new List<Product>()
            {
                new Product(){Id=1, CategoryId=1, Model="Cross Focus", Year=2021, Price=5099d, Description="26\" 15\" 2021 Gray-blue (26CWS21-003327)"},
                new Product(){Id=2, CategoryId=1, Model="Ardis Vincent", Year=2021, Price=8649d, Description="26\" 19.5\" 2021 Серо-оранжевый (0134)"},
                new Product(){Id=3, CategoryId=1, Model="Crossride Skyline", Year=2020, Price=4302d, Description="24\" 13\" 2020 Оранжевый (02391-П)"},
                new Product(){Id=4, CategoryId=1, Model="Ardis City Line", Year=2019, Price=4590d, Description="24\" 15\" 2019 Темно-зеленый (0493)"},
                new Product(){Id=5, CategoryId=2, Model="Warm socks Cairn SPIRIT", Year=2021, Price=550d, Description="35/38 Black Fuchsia (0.50717.6260)"},
                new Product(){Id=6, CategoryId=2, Model="Reflective vest ONRIDE Loadstar", Year=2021, Price=784d, Description="Color - Light green, Category - Unisex"},
                new Product(){Id=7, CategoryId=2, Model="Costume bike X-Тiger XM-CT-01302", Year=2021, Price=1095d, Description="Red S jacket with long sleeve pants"},
                new Product(){Id=8, CategoryId=2, Model="Sleeveless vest X-Тiger XM-WGY-00101", Year=2021, Price=446d, Description="Green XXL Bicycle Jacket"},
                new Product(){Id=9, CategoryId=3, Model="Shoe covers Orca Aero Shoe", Year=2021, Price=1372d, Description="Cover M/L Black (FVA45401)"},
                new Product(){Id=10, CategoryId=3, Model="Bike shoes SHIMANO ET500", Year=2021, Price=3014d, Description="black, size EU46"},
                new Product(){Id=11, CategoryId=3, Model="Women's Highway Bike Shoes Shimano SH-RP301", Year=2021, Price=1499d, Description="white EU 39"},
                new Product(){Id=12, CategoryId=3, Model="Cycling road bikes for men dhb Troika", Year=2021, Price=1499d, Description="white EU 43"}
            };
            modelBuilder.Entity<Product>().HasData(products);
            base.OnModelCreating(modelBuilder);
        }
    }
}
