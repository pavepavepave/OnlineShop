using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using System;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index()
        {
            var cart = await cartsRepository.TryGetByUserIdAsync(User.Identity.Name);
            if (cart == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(Mapping.ToCartViewModel(cart));
        }

        public async Task<IActionResult> Add(Guid id)
        {
            var product = await productsRepository.TryGetByIdAsync(id);
            if (product == null)
            {
                return RedirectToAction("Error", "Home");
            }
            await cartsRepository.AddAsync(product, User.Identity.Name);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Clear()
        {
            await cartsRepository.ClearAsync(User.Identity.Name);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ReduceAmount(Guid id)
        {
            await cartsRepository.ReduceAmountAsync(id, User.Identity.Name);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> IncreaseAmount(Guid id)
        {
            await cartsRepository.IncreaseAmountAsync(id, User.Identity.Name);
            return RedirectToAction("Index");
        }
    }
}