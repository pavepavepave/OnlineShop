using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class ChangePasswordVM
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Не указан логин.")]
        [StringLength(16, MinimumLength = 5, ErrorMessage = "Логин должен содержать от 5 до 16 символов")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не указан пароль.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Пароль должен содержать от 6 до 30 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Не указано подтверждение пароля.")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
    }
}