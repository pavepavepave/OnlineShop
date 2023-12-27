using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class OrderController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrderController(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<IActionResult> Index()
        {
            var ordersList = await _ordersRepository.GetAllAsync();
            return View(Mapping.ToOrderViewModels(ordersList));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var order = await _ordersRepository.GetByOrderIdAsync(id);
            return View(Mapping.ToOrderViewModel(order));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, OrderStatusVM status)
        {
            await _ordersRepository.EditAsync(id, (int)status);
            return RedirectToAction(nameof(Index));
        }
    }
}