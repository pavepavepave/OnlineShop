using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrdersRepository ordersRepository;
        private readonly ICartsRepository cartRepository;

        public OrderController(IOrdersRepository ordersRepository, ICartsRepository cartRepository)
        {
            this.ordersRepository = ordersRepository;
            this.cartRepository = cartRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int id)
        {
            var order = ordersRepository.GetByOrderId(id);
            return View(Mapping.ToOrderViewModel(order));
        }

        [HttpPost]
        public ActionResult Create(OrderVM orderVM)
        {
            var existingCart = cartRepository.TryGetByUserId(Constants.UserId);
            if (ModelState.IsValid)
            {
                var order = Mapping.ToOrder(orderVM, existingCart, Constants.UserId);
                ordersRepository.Create(order);
                cartRepository.Clear(Constants.UserId);
                return View("Detail", Mapping.ToOrderViewModel(order));
            };
            return View(orderVM);
        }
    }
}