using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OnlineShopWebApp.Helpers.Interfaces;

namespace OnlineShopWebApp.Helpers
{
    public class ImagesProvider: IImagesProvider
    {
        private readonly IWebHostEnvironment _appEnvironment;

        public ImagesProvider(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public async Task<List<string>> SafeFiles(IFormFile[] files, ImageFolders folder)
        {
            var imagesPaths = new List<string>();
            foreach (var file in files)
            {
                var imagePath = await SafeFile(file, folder);
                imagesPaths.Add(imagePath);
            }

            return imagesPaths;
        }

        public async Task<string> SafeFile(IFormFile file, ImageFolders folder)
        {
            if (file == null) return null;
            
            var folderPath = Path.Combine(_appEnvironment.WebRootPath + "/images/" + folder);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileName = Guid.NewGuid() + "." + file.FileName.Split('.').Last();
            var path = Path.Combine(folderPath, fileName);
            await using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return "/images/" + folder + "/" + fileName;
        }
    }
}