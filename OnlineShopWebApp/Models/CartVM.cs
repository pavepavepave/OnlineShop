using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.Models
{
    public class CartVM
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<CartItemVM> Items { get; set; }
        public decimal Cost
        {
            get
            {
                return Items?.Sum(x => x.Cost) ?? 0;
            }
        }
        public decimal Amount
        {
            get
            {
                return Items?.Sum(x => x.Amount) ?? 0;
            }
        }
    }
}