using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class IdentityInitializer
    {
        public static void Initialize(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            const string adminName = "Administrator";
            const string adminEmail = "adminmail@mailmail.com";
            const string adminPassword = "_Aa12345678";

            if (roleManager.FindByNameAsync(Constants.AdminRoleName).Result == null) //если роли админа нет, то создаем
            {
                roleManager.CreateAsync(new IdentityRole(Constants.AdminRoleName)).Wait();
            }

            if (roleManager.FindByNameAsync(Constants.UserRoleName).Result == null) //если роли юзера нет, то создаем
            {
                roleManager.CreateAsync(new IdentityRole(Constants.UserRoleName)).Wait();
            }

            if (userManager.FindByNameAsync(adminName).Result == null) //если нет пользователя 'admin', то создадим его.
            {
                var admin = new User { Email = adminEmail, UserName = adminName };
                var result = userManager.CreateAsync(admin, adminPassword).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(admin, Constants.AdminRoleName).Wait(); //Добавляем юзера админа в группу ролей админов
                }
            }
        }
    }
}
