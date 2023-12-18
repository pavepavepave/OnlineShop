using Microsoft.AspNetCore.Identity;
using System;

namespace OnlineShop.Db.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime DateCreate { get; set; }
        public User()
        {
            DateCreate = DateTime.Now;
        }
    }
}
