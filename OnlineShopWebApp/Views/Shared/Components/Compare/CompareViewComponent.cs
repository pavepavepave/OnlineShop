using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using System.Linq;

namespace OnlineShopWebApp.Views.Shared.Components.Cart
{
    public class CompareViewComponent : ViewComponent
    {
        private readonly ICompareRepository compareRepository;

        public CompareViewComponent(ICompareRepository compareRepository)
        {
            this.compareRepository = compareRepository;
        }

        public IViewComponentResult Invoke()
        {
            var compareCounts = compareRepository.GetAll(Constants.UserId).Count();
            return View("Compare", compareCounts);
        }
    }
}