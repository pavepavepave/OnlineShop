using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class AccountVM
    {
        [Required(ErrorMessage = "Не указан логин.")]
        [StringLength(16, MinimumLength = 5, ErrorMessage = "Логин должен содержать от 5 до 16 символов")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Имя должно содержать от 2 до 30 символов")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Фамилия должна содержать от 6 до 30 символов")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Не указан email.")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Пароль должен содержать от 6 до 30 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Не указано подтверждение пароля.")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
        public string ReturnUrl { get; set; }
    }
}