using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly ICartsRepository _cartRepository;

        public OrderController(IOrdersRepository ordersRepository, ICartsRepository cartRepository)
        {
            _ordersRepository = ordersRepository;
            _cartRepository = cartRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detail(int id)
        {
            var order = await _ordersRepository.GetByOrderIdAsync(id);
            return View(Mapping.ToOrderViewModel(order));
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderVM orderVM)
        {
            var existingCart = await _cartRepository.TryGetByUserIdAsync(Constants.UserId);
            if (ModelState.IsValid)
            {
                var order = Mapping.ToOrder(orderVM, existingCart, Constants.UserId);
                await _ordersRepository.CreateAsync(order);
                await _cartRepository.ClearAsync(Constants.UserId);
                return View("Detail", Mapping.ToOrderViewModel(order));
            };
            return View(orderVM);
        }
    }
}