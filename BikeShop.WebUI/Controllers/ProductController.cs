using BikeShop.Domain;
using BikeShop.WebUI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShop.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly BikeShopContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<Program> _logger;

        public ProductController(BikeShopContext context, IWebHostEnvironment env, ILogger<Program> logger)
        {
            _context = context;
            _environment = env;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string category, int? page)
        {
            int pageSize = 3;
            IEnumerable<Product> products = await _context.Products.Include(p=>p.Category).Include(p => p.ImageFiles).ToListAsync();
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

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Model,Year,Description,Price,CategoryId")] Product product, IFormFileCollection UploadedFiles)
        {
            if (ModelState.IsValid)
            {
                int maxIdImages = _context.ImageFiles.OrderByDescending(i => i.Id).FirstOrDefault()?.Id ?? 0;
                foreach (var uploadedFile in UploadedFiles)
                {
                    if (uploadedFile != null)
                    {
                        ImageFile file = await AddNewImage(uploadedFile, _environment, ++maxIdImages);
                        product.ImageFiles.Add(file);
                        _context.ImageFiles.Add(file);
                    }
                }
                _context.Add(product);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ProductViewModel viewModel = new ProductViewModel
            {
                Product = product
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(p => p.ImageFiles).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ProductViewModel viewModel = new ProductViewModel
            {
                Product = product
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model,Year,Description,Price,CategoryId")] Product product, IFormFileCollection UploadedFiles, string[] files)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Product oldProduct= await _context.Products.Include(p => p.ImageFiles).FirstOrDefaultAsync(p => p.Id == id);
                    if (oldProduct != null)
                    {
                        int i = 0;
                        while (i < oldProduct.ImageFiles.Count)
                        {
                            if (oldProduct.ImageFiles[i].Path != files[i])
                            {
                                _context.Update(await AddImage(UploadedFiles.FirstOrDefault(f => f.FileName == files[i]), _environment, oldProduct.ImageFiles[i]));
                            }
                            i++;
                        }
                        int maxIdImages = _context.ImageFiles.OrderByDescending(i => i.Id).FirstOrDefault()?.Id ?? 0;
                        while (i < files.Length)
                        {
                            if (files[i] != null)
                            {
                                ImageFile image = await AddNewImage(UploadedFiles.FirstOrDefault(f => f.FileName == files[i]), _environment, ++maxIdImages);
                                image.ProductId = oldProduct.Id;
                                _context.ImageFiles.Add(image);
                            }
                            i++;
                        }
                        oldProduct.CategoryId = product.CategoryId;
                        oldProduct.Description = product.Description;
                        oldProduct.Model = product.Model;
                        oldProduct.Price = product.Price;
                        oldProduct.Year = product.Year;

                        _context.Update(oldProduct);
                        int k = 12;
                        try
                        {
                            await _context.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"An error occurred editing the Product with id={oldProduct.Id}");
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            var files = await _context.ImageFiles.Where(f => f.ProductId == product.Id).ToListAsync();
            files.ForEach(f =>
            {
                _context.Entry<ImageFile>(f).State = EntityState.Deleted;
                System.IO.File.Delete(_environment.WebRootPath + f.Path);
            });
            _context.Entry<Product>(product).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
        private async Task<ImageFile> AddImage(IFormFile formFile, IWebHostEnvironment env, ImageFile image)
        {
            using (var fileStream = new FileStream(_environment.WebRootPath + image.Path, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }
            image.Name = formFile.FileName;
            return image;
        }
        private async Task<ImageFile> AddNewImage(IFormFile formFile, IWebHostEnvironment env, int fileName)
        {
            ImageFile image = new ImageFile();
            string formatFile = formFile.FileName.Substring(formFile.FileName.LastIndexOf('.'));
            image.Path = "/Files/" + fileName.ToString() + formatFile;
            return await AddImage(formFile, env, image);
        }
    }
}
