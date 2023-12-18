using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class RoleVM
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Укажите название роли")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Длина роли от 4 до 20 символов")]
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var role = (RoleVM)obj;
            return Name == role.Name;    
        }
    }
}