using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductsRepository productsRepository;

        public ShopController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await productsRepository.GetAllAsync();
            return View(Mapping.ToProductsVM(products));
        }
    }
}