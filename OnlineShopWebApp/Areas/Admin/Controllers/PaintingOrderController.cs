using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using System.Threading.Tasks;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class PaintingOrderController : Controller
    {
        private readonly IPaintingOrderRepository _paintingOrderRepository;

        public PaintingOrderController(IPaintingOrderRepository paintingOrderRepository)
        {
            _paintingOrderRepository = paintingOrderRepository;
        }

        public async Task<IActionResult> Index()
        {
            var ordersList = await _paintingOrderRepository.GetAllAsync();
            return View(Mapping.ToPaintingOrderViewModels(ordersList));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var order = await _paintingOrderRepository.GetByPaintingOrderIdAsync(id);
            return View(Mapping.ToPaintingOrderViewModel(order));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PaintingOrderVM paintingOrderVM, OrderStatusVM status)
        {
            await _paintingOrderRepository.EditPaintingOrderAsync(id, Mapping.ToPaintingOrder(paintingOrderVM, paintingOrderVM.UserId), (int)status);
            return RedirectToAction(nameof(Index));
        }
    }
}