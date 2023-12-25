using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using System;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ProductController : Controller
    {
        private readonly IProductsRepository productsRepository;

        public ProductController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await productsRepository.GetAllAsync();
            return View(Mapping.ToProductViewModels(products));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await productsRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductVM product)
        {
            if (ModelState.IsValid)
            {
                var productDb = new Product
                {
                    Name = product.Name,
                    Cost = product.Cost,
                    Description = product.Description,
                    Image = @"/images/paints/images.jpg"
                };
                await productsRepository.AddAsync(productDb);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var productDb = await productsRepository.TryGetByIdAsync(id);
            if (productDb == null)
            {
                return NotFound();
            }

            var productVM = new ProductVM
            {
                Id = productDb.Id,
                Name = productDb.Name,
                Cost = productDb.Cost,
                Description = productDb.Description,
                Image = productDb.Image
            };

            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var product = await productsRepository.TryGetByIdAsync(productVM.Id);
                product.Name = productVM.Name;
                product.Cost = productVM.Cost;
                product.Description = productVM.Description;
                product.Image = productVM.Image;

                await productsRepository.EditAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(productVM);
        }
    }
}