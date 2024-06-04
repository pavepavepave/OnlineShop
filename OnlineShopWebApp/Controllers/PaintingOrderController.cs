using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class PaintingOrderController : Controller
    {
        private readonly IPaintingOrderRepository _paintingOrderRepository;

        public PaintingOrderController(IPaintingOrderRepository paintingOrderRepository)
        {
            _paintingOrderRepository = paintingOrderRepository;
        }
        
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Index(PaintingOrderVM paintingOrderVM)
        {
            if (ModelState.IsValid)
            {
                var order = Mapping.ToPaintingOrder(paintingOrderVM, User.Identity.Name);
                await _paintingOrderRepository.CreateAsync(order);
                return View("Detail", Mapping.ToPaintingOrderViewModel(order));
            };
            
            return View(paintingOrderVM);
        }
        
        public async Task<IActionResult> Detail(int id)
        {
            var order = await _paintingOrderRepository.GetByPaintingOrderIdAsync(id);
            return View(Mapping.ToPaintingOrderViewModel(order));
        }
    }
}