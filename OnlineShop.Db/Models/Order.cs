using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<CartItem> Items { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateOrder { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Order()
        {
            DateOrder = DateTime.Now;
            Status = OrderStatus.Created;
        }
    }
}