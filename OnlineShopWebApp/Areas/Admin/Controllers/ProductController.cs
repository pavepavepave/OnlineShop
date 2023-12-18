using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using System;

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

        public ActionResult Index()
        {
            var products = productsRepository.GetAll();
            return View(Mapping.ToProductViewModels(products));
        }

        public ActionResult Delete(Guid id)
        {
            productsRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductVM product)
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
                productsRepository.Add(productDb);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public IActionResult Edit(Guid id)
        {
            var productDb = productsRepository.TryGetById(id);
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
        public IActionResult Edit(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var product = productsRepository.TryGetById(productVM.Id);
                product.Name = productVM.Name;
                product.Cost = productVM.Cost;
                product.Description = productVM.Description;
                product.Image = productVM.Image;

                productsRepository.Edit(product);
                return RedirectToAction(nameof(Index));
            }
            return View(productVM);
        }
    }
}