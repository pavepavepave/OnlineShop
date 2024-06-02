using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Views.Shared.Components.Cart
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartsRepository cartsRepository;

        public CartViewComponent(ICartsRepository cartsRepository)
        {
            this.cartsRepository = cartsRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = await cartsRepository.TryGetByUserIdAsync(User.Identity.Name);
            var cartVM = Mapping.ToCartViewModel(cart);
            var productCounts = cartVM?.Amount ?? 0;
            return View("Cart", productCounts);
        }
    }
}