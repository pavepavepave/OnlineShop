using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Controllers;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IStringLocalizer<AuthorizationController> _identityErrorsLocalizer;

        public UserController(UserManager<User> usersManager, IStringLocalizer<AuthorizationController> identityErrorsLocalizer, RoleManager<IdentityRole> roleManager)
        {
            _userManager = usersManager;
            _identityErrorsLocalizer = identityErrorsLocalizer;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var viewModels = users.Select(x => x.ToUserViewModel()).ToList();
            return View(viewModels);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(AccountVM registration)
        {
            if (registration.UserName == registration.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать.");
            }

            if (await _userManager.FindByNameAsync(registration.UserName) != null)
            {
                ModelState.AddModelError("", "Такой пользователь уже существует.");
            }

            if (await _userManager.FindByEmailAsync(registration.Email) != null)
            {
                ModelState.AddModelError("", "Email уже занят.");
            }

            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    UserName = registration.UserName,
                    FirstName = registration.FirstName,
                    SecondName = registration.SecondName,
                    Email = registration.Email,
                };
                var result = await _userManager.CreateAsync(user, registration.Password);
                if (result.Succeeded)
                {
                    Log.Logger.Information($"{user.UserName} зарегистрирован через админ панель.");
                    await TryAsignUserRole(user);
                    return View(nameof(Info), user.ToUserViewModel());
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        var errorMessage = _identityErrorsLocalizer[error.Code]?.Value ?? error.Description;
                        ModelState.AddModelError("", errorMessage);
                    }
                }
            }
            return View(registration);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Info(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user.ToUserViewModel());
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var changePassword = user.ToChangePasswordViewModel();
                return View(changePassword);
            }
            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM changePassword)
        {
            if (changePassword.UserName == changePassword.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать.");
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(changePassword.Id);
                var newPassword = _userManager.PasswordHasher.HashPassword(user, changePassword.Password);
                user.PasswordHash = newPassword;
                return RedirectToAction(nameof(Info), new { id = changePassword.Id });
            }
            return View(changePassword);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var editUser = user.ToEditUserViewModel();
                return View(editUser);
            }
            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        public async Task<IActionResult> Edit(EditUserVM editUser)
        {
            var user = await _userManager.FindByIdAsync(editUser.Id);
            var passwordHasher = new PasswordHasher<User>();
            if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, editUser.UserName) == PasswordVerificationResult.Success)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать.");
            }

            if (ModelState.IsValid)
            {
                user.Id = editUser.Id;
                user.UserName = editUser.UserName;
                user.FirstName = editUser.FirstName;
                user.SecondName = editUser.SecondName;
                user.Email = editUser.Email;

                await _userManager.UpdateAsync(user);

                return RedirectToAction(nameof(Info), new { id = editUser.Id });
            }
            return View(editUser);
        }

        public async Task<IActionResult> ChangeRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var roles = _roleManager.Roles.ToList();
                var model = new ChangeRoleVM
                {
                    UserName = user.UserName,
                    UserRoles = userRoles.Select(x => new RoleVM { Name = x }).ToList(),
                    AllRoles = roles.Select(x => new RoleVM { Name = x.Name }).ToList()
                };
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(string id, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user); // получем список ролей пользователя
                var allRoles = _roleManager.Roles.ToList(); // получаем все роли
                var addedRoles = roles.Except(userRoles); // получаем список ролей, которые были добавлены
                var removedRoles = userRoles.Except(roles); // получаем роли, которые были удалены

                await _userManager.AddToRolesAsync(user, addedRoles);
                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction(nameof(Info), new { id = user.Id });
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task TryAsignUserRole(User user)
        {
            try
            {
                await _userManager.AddToRoleAsync(user, Constants.UserRoleName);
            }
            catch (Exception)
            {
                Log.Logger.Information($"{user.UserName} не был добавлен в группу пользователей.");
            }
        }
    }
}