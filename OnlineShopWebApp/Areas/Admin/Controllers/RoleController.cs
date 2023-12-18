using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db;
using OnlineShopWebApp.Areas.Admin.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var viewModels = roles.Select(x => new RoleVM { Id = x.Id, Name = x.Name }).ToList();
            return View(viewModels);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(RoleVM roleVM)
        {
            if (await _roleManager.FindByNameAsync(roleVM.Name) != null)
            {
                ModelState.AddModelError("", "Такая роль уже существует.");
            }

            if (!char.IsLetter(roleVM.Name[0]))
            {
                ModelState.AddModelError("", "Роль должна начинаться только с буквы.");
            }

            if (roleVM.Name.Any(x=>char.IsDigit(x)))
            {
                ModelState.AddModelError("", "Роль не должна содержать цифры.");
            }

            if (ModelState.IsValid)
            {
                var user = new IdentityRole()
                {
                    Name = roleVM.Name,
                };
                var result = await _roleManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Ошибка добавления");
            }
            return View(roleVM);
        }

        public async Task<ActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}