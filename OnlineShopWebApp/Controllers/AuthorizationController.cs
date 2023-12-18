using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using Serilog;
using System;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly UserManager<User> _userManager; //менеджер для работы с пользователями
        private readonly SignInManager<User> _signInManager; //хранение куки о авторизованных юзерах
        private readonly IStringLocalizer<AuthorizationController> _identityErrorsLocalizer;

        public AuthorizationController(UserManager<User> userManager, SignInManager<User> signInManager,
            IStringLocalizer<AuthorizationController> identityErrorsLocalizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identityErrorsLocalizer = identityErrorsLocalizer;
        }

        public ActionResult Login(string returnUrl)
        {
            return View(new LoginVM { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, login.IsRemember, false);

                if (result.Succeeded)
                {
                    return Redirect(login.ReturnUrl ?? "/Home");
                }

                ModelState.AddModelError("", "Не удалось выполнить вход. Пожалуйста, проверьте введенные данные.");
            }

            return View(login);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registration(string returnUrl)
        {
            return View(new AccountVM { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<ActionResult> Registration(AccountVM registration)
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
                var user = new User
                {
                    UserName = registration.UserName,
                    FirstName = registration.FirstName,
                    SecondName = registration.SecondName,
                    Email = registration.Email,
                };
                var result = await _userManager.CreateAsync(user, registration.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    Log.Logger.Information($"{user.UserName} зарегистрирован.");
                    await TryAsignUserRole(user);
                    return Redirect(registration.ReturnUrl ?? "/Home");
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
        public async Task TryAsignUserRole(User user)
        {
            try
            {
                await _userManager.AddToRoleAsync(user, OnlineShop.Db.Constants.UserRoleName);
            }
            catch (Exception)
            {
                Log.Logger.Information($"{user.UserName} не был добавлен в группу пользователей.");
            }
        }
    }
}