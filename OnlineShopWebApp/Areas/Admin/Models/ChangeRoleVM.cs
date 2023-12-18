using System.Collections.Generic;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class ChangeRoleVM
    {
        public string UserName { get; set; }
        public List<RoleVM> UserRoles { get; set;}
        public List<RoleVM> AllRoles { get; set; }
    }
}
