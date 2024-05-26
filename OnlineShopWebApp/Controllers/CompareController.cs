using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using System;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class CompareController : Controller
    {
        private readonly ICompareRepository compareRepository;
        private readonly IProductsRepository productsRepository;

        public CompareController(ICompareRepository compareRepository, IProductsRepository productsRepository)
        {
            this.compareRepository = compareRepository;
            this.productsRepository = productsRepository;
        }

        public IActionResult Index()
        {
            var products = compareRepository.GetAll(Constants.UserId);
            return View(Mapping.ToProductsVM(products));
        }

        public IActionResult Add(Guid id)
        {
            compareRepository.Add(Constants.UserId, id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid id)
        {
            compareRepository.Delete(Constants.UserId, id);
            return RedirectToAction(nameof(Index));
        }
    }
}