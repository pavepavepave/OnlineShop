using System;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class UserVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateCreate { get; set; }
    }
}