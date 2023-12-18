using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.Models
{
    public class OrderVM
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<CartItemVM> Items { get; set; }
        public OrderStatusVM Status { get; set; }
        public DateTime DateOrder { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public decimal Cost
        {
            get => Items?.Sum(x => x.Cost) ?? 0;
        }
    }
}