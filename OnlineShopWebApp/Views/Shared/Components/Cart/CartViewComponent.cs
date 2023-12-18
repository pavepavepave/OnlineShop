using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Views.Shared.Components.Cart
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartsRepository cartsRepository;

        public CartViewComponent(ICartsRepository cartsRepository)
        {
            this.cartsRepository = cartsRepository;
        }

        public IViewComponentResult Invoke()
        {
            var cart = cartsRepository.TryGetByUserId(Constants.UserId);
            var cartVM = Mapping.ToCartViewModel(cart);
            var productCounts = cartVM?.Amount ?? 0;
            return View("Cart", productCounts); //возвращаем частичное представление Cart, в которое передали значение productCounts
        }
    }
}