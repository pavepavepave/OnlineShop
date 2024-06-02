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
    public class PaintingController : Controller
    {
        private readonly IPaintingsRepository paintingRepository;
        private readonly IImagesProvider _imagesProvider;

        public PaintingController(IPaintingsRepository paintingRepository, IImagesProvider imagesProvider)
        {
            this.paintingRepository = paintingRepository;
            _imagesProvider = imagesProvider;
        }

        public async Task<IActionResult> Index()
        {
            var paintings = await paintingRepository.GetAllAsync();
            return View(Mapping.ToPaintingsVM(paintings));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await paintingRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPaintingVM painting)
        {
            if (!ModelState.IsValid) return View(painting);

            List<string> imagesPaths;
            if (painting.UploadedFiles == null || painting.UploadedFiles.Length == 0)
            {
                imagesPaths = new List<string> { "/images/paintings/defaultImage.jpg" };
            }
            else
            {
                imagesPaths = await _imagesProvider.SafeFiles(painting.UploadedFiles.ToArray(), ImageFolders.Goods);
            }

            await paintingRepository.AddAsync(painting.ToPainting(imagesPaths));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var painting = await paintingRepository.TryGetByIdAsync(id);
            if (painting == null)
            {
                return NotFound();
            }

            return View(painting.ToEditPaintingVM());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPaintingVM painting)
        {
            if (ModelState.IsValid)
            {
                var addedImagesPaths = await _imagesProvider.SafeFiles(painting.UploadedFiles, ImageFolders.Goods);
                painting.ImagePath = addedImagesPaths;
                
                await paintingRepository.EditAsync(painting.ToPainting(), painting.UploadedFiles);
                
                return RedirectToAction(nameof(Index));
            }
            return View(painting);
        }
    }
}