using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopWebApp.Helpers.Interfaces;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ProductController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly IImagesProvider _imagesProvider;

        public ProductController(IProductsRepository productsRepository, IImagesProvider imagesProvider)
        {
            this.productsRepository = productsRepository;
            _imagesProvider = imagesProvider;
        }

        public async Task<IActionResult> Index()
        {
            var products = await productsRepository.GetAllAsync();
            return View(Mapping.ToProductsVM(products));
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
        public async Task<IActionResult> Add(AddProductVM product)
        {
            if (!ModelState.IsValid) return View(product);

            List<string> imagesPaths;
            if (product.UploadedFiles == null || product.UploadedFiles.Length == 0)
            {
                imagesPaths = new List<string> { "/images/goods/defaultImage.jpg" };
            }
            else
            {
                imagesPaths = await _imagesProvider.SafeFiles(product.UploadedFiles.ToArray(), ImageFolders.Goods);
            }

            await productsRepository.AddAsync(product.ToProduct(imagesPaths));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await productsRepository.TryGetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product.ToEditProductVM());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductVM product)
        {
            if (ModelState.IsValid)
            {
                var addedImagesPaths = await _imagesProvider.SafeFiles(product.UploadedFiles, ImageFolders.Goods);
                product.ImagePath = addedImagesPaths;
                
                await productsRepository.EditAsync(product.ToProduct(), product.UploadedFiles);
                
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
    }
}