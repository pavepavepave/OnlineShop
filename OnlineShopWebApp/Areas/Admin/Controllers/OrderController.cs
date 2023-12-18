using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class OrderController : Controller
    {
        private readonly IOrdersRepository ordersRepository;

        public OrderController(IOrdersRepository ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public ActionResult Index()
        {
            var ordersList = ordersRepository.GetAll();
            return View(Mapping.ToOrderViewModels(ordersList));
        }

        public ActionResult Detail(int id)
        {
            var order = ordersRepository.GetByOrderId(id);
            return View(Mapping.ToOrderViewModel(order));
        }

        [HttpPost]
        public ActionResult Edit(int id, OrderStatusVM status)
        {
            ordersRepository.Edit(id, (int)status);
            return RedirectToAction(nameof(Index));
        }
    }
}