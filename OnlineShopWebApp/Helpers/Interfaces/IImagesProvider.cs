using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineShopWebApp.Helpers.Interfaces
{
    public interface IImagesProvider
    {
        Task<List<string>> SafeFiles(IFormFile[] files, ImageFolders folder);
        Task<string> SafeFile(IFormFile file, ImageFolders folder);
    }
}