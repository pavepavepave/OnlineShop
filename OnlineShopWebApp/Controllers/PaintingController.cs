using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using System;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    public class PaintingController : Controller
    {
        private readonly IPaintingsRepository _paintingsRepository;

        public PaintingController(IPaintingsRepository paintingsRepository)
        {
            _paintingsRepository = paintingsRepository;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var painting = await _paintingsRepository.TryGetByIdAsync(id);
            return View(Mapping.ToPaintingVM(painting));
        }
    }
}