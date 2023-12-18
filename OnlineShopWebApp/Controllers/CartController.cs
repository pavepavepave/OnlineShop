using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using System;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartsRepository cartsRepository;
        private readonly IProductsRepository productsRepository;

        public CartController(ICartsRepository cartsRepository, IProductsRepository productsRepository)
        {
            this.cartsRepository = cartsRepository;
            this.productsRepository = productsRepository;
        }

        public IActionResult Index()
        {
            var cart = cartsRepository.TryGetByUserId(Constants.UserId);
            if (cart == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(Mapping.ToCartViewModel(cart));
        }

        public IActionResult Add(Guid id)
        {
            var product = productsRepository.TryGetById(id);
            if (product == null)
            {
                return RedirectToAction("Error", "Home");
            }
            cartsRepository.Add(product, Constants.UserId);
            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            cartsRepository.Clear(Constants.UserId);
            return RedirectToAction("Index");
        }

        public IActionResult ReduceAmount(Guid id)
        {
            cartsRepository.ReduceAmount(id, Constants.UserId);
            return RedirectToAction("Index");
        }

        public IActionResult IncreaseAmount(Guid id)
        {
            cartsRepository.IncreaseAmount(id, Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}