using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using System;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly IFavoritesRepository favoritesRepository;
        private readonly IProductsRepository productsRepository;

        public FavoritesController(IFavoritesRepository favoritesRepository, IProductsRepository productsRepository)
        {
            this.favoritesRepository = favoritesRepository;
            this.productsRepository = productsRepository;
        }

        public IActionResult Index()
        {
            var products = favoritesRepository.GetAll(Constants.UserId);
            return View(Mapping.ToProductsVM(products));
        }

        public IActionResult Add(Guid id)
        {
            favoritesRepository.Add(Constants.UserId, id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid id)
        {
            favoritesRepository.Delete(Constants.UserId, id);
            return RedirectToAction(nameof(Index));
        }
    }
}