using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
 
    }
}