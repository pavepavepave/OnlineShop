using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly IPaintingsRepository _paintingsRepository;

        public PortfolioController(IPaintingsRepository paintingsRepository)
        {
            _paintingsRepository = paintingsRepository;
        }

        public async Task<IActionResult> Index()
        {
            var paintings = await _paintingsRepository.GetAllAsync();
            return View(Mapping.ToPaintingsVM(paintings));
        }
    }
}