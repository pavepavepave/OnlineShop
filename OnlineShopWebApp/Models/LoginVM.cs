using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Не указан логин.")]
        [StringLength(16, MinimumLength = 5, ErrorMessage = "Логин должен содержать от 5 до 16 символов")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не указан пароль.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Пароль должен содержать от 6 до 30 символов")]
        public string Password { get; set; }

        public bool IsRemember { get; set; }
        public string ReturnUrl { get; set; }
    }
}