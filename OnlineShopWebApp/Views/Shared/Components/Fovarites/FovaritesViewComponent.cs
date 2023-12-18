using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using System.Linq;

namespace OnlineShopWebApp.Views.Shared.Components.Cart
{
    public class FovaritesViewComponent : ViewComponent
    {
        private readonly IFavoritesRepository favoritesRepository;

        public FovaritesViewComponent(IFavoritesRepository favoritesRepository)
        {
            this.favoritesRepository = favoritesRepository;
        }

        public IViewComponentResult Invoke()
        {
            var count = favoritesRepository.GetAll(Constants.UserId).Count();
            return View("Fovarites", count);
        }
    }
}