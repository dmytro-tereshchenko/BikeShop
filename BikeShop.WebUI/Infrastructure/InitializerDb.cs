using BikeShop.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.WebUI.Infrastructure
{
    public static class InitializerDb
    {
        public static void Initialize(BikeShopContext context, IWebHostEnvironment appEnvironment, string initialFilesPath)
        {
            if (!context.Users.Any())
            {
                context.Roles.ToList().Select(r => context.Entry<Role>(r).State = EntityState.Deleted);
                List<Role> roles = new List<Role>()
                {
                    new Role(){ Name="admin" },
                    new Role(){ Name="user" }
                };
                context.Roles.AddRange(roles);
                List<User> users = new List<User>()
                {
                    new User(){ Email="admin@gmail.com", Role=roles[0], Password="admin" },
                    new User(){ Email="user@gmail.com", Role=roles[1], Password="user" }
                };
                context.Users.AddRange(users);
                context.Categories.ToList().Select(c => context.Entry<Category>(c).State = EntityState.Deleted);
                List<Category> categories = new List<Category>()
                {
                    new Category(){ Name="Bicycles" },
                    new Category(){ Name="Cycling clothes" },
                    new Category(){ Name="Bicycle shoes" }
                };
                context.Categories.AddRange(categories);
                context.Products.ToList().Select(p => context.Entry<Product>(p).State = EntityState.Deleted);
                List<Product> products = new List<Product>()
                {
                    new Product(){ Category=categories[0], Model="Cross Focus", Year=2021, Price=5099d, Description="26\" 15\" 2021 Gray-blue (26CWS21-003327)"},
                    new Product(){ Category=categories[0], Model="Ardis Vincent", Year=2021, Price=8649d, Description="26\" 19.5\" 2021 Gray-orange (0134)"},
                    new Product(){ Category=categories[0], Model="Crossride Skyline", Year=2020, Price=4302d, Description="24\" 13\" 2020 Orange (02391-П)"},
                    new Product(){ Category=categories[0], Model="Ardis City Line", Year=2019, Price=4590d, Description="24\" 15\" 2019 Dark green (0493)"},
                    new Product(){ Category=categories[1], Model="Warm socks Cairn SPIRIT", Year=2021, Price=550d, Description="35/38 Black Fuchsia (0.50717.6260)"},
                    new Product(){ Category=categories[1], Model="Reflective vest ONRIDE Loadstar", Year=2021, Price=784d, Description="Color - Light green, Category - Unisex"},
                    new Product(){ Category=categories[1], Model="Costume bike X-Тiger XM-CT-01302", Year=2021, Price=1095d, Description="Red S jacket with long sleeve pants"},
                    new Product(){ Category=categories[1], Model="Sleeveless vest X-Тiger XM-WGY-00101", Year=2021, Price=446d, Description="Green XXL Bicycle Jacket"},
                    new Product(){ Category=categories[2], Model="Shoe covers Orca Aero Shoe", Year=2021, Price=1372d, Description="Cover M/L Black (FVA45401)"},
                    new Product(){ Category=categories[2], Model="Bike shoes SHIMANO ET500", Year=2021, Price=3014d, Description="black, size EU46"},
                    new Product(){ Category=categories[2], Model="Women's Highway Bike Shoes Shimano SH-RP301", Year=2021, Price=1499d, Description="white EU 39"},
                    new Product(){ Category=categories[2], Model="Cycling road bikes for men dhb Troika", Year=2021, Price=1499d, Description="white EU 43"}
                };
                context.Products.AddRange(products);
                context.ImageFiles.ToList().Select(c => context.Entry<ImageFile>(c).State = EntityState.Deleted);
                int maxIdImages = context.ImageFiles.OrderByDescending(i => i.Id).FirstOrDefault()?.Id ?? 0;
                List<ImageFile> images = new List<ImageFile>()
                {
                    InitialImage(appEnvironment, initialFilesPath, products[0], ++maxIdImages, "Cross Focus 2021 Gray-blue.jpg"),
                    InitialImage(appEnvironment, initialFilesPath, products[1], ++maxIdImages, "Ardis Vincent 2021 Gray-orange.jpg"),
                    InitialImage(appEnvironment, initialFilesPath, products[2], ++maxIdImages, "Crossride Skyline.jpg"),
                    InitialImage(appEnvironment, initialFilesPath, products[3], ++maxIdImages, "Ardis City Line.jpg"),
                    InitialImage(appEnvironment, initialFilesPath, products[4], ++maxIdImages, "Warm socks Cairn SPIRIT.jpg"),
                    InitialImage(appEnvironment, initialFilesPath, products[5], ++maxIdImages, "Reflective vest ONRIDE Loadstar.jpg"),
                    InitialImage(appEnvironment, initialFilesPath, products[6], ++maxIdImages, "Costume bike X-Тiger XM-CT-01302.jpg"),
                    InitialImage(appEnvironment, initialFilesPath, products[7], ++maxIdImages, "Sleeveless vest X-Тiger XM-WGY-00101.jpg"),
                    InitialImage(appEnvironment, initialFilesPath, products[8], ++maxIdImages, "Shoe covers Orca Aero Shoe.jpg"),
                    InitialImage(appEnvironment, initialFilesPath, products[9], ++maxIdImages, "Bike shoes SHIMANO ET500.jpg"),
                    InitialImage(appEnvironment, initialFilesPath, products[10], ++maxIdImages, "Women's Highway Bike Shoes Shimano SH-RP301.jpg"),
                    InitialImage(appEnvironment, initialFilesPath, products[11], ++maxIdImages, "Cycling road bikes for men dhb Troika.jpg")
                };
                context.ImageFiles.AddRange(images);
                context.SaveChanges();
            }
        }
        private static ImageFile InitialImage(IWebHostEnvironment appEnvironment, string initialFilesPath, Product product, int targetName, string sourseFile)
        {
            string formatFile = sourseFile.Substring(sourseFile.LastIndexOf('.'));
            string sourcePath = appEnvironment.ContentRootPath + initialFilesPath + sourseFile;
            string targetPath = "/Files/" + targetName.ToString() + formatFile;
            File.Copy(sourcePath, appEnvironment.WebRootPath + targetPath, true);
            return new ImageFile
            {
                Name = sourseFile,
                Path = targetPath,
                Product = product
            };
        }
    }
}
