using System;

namespace OnlineShop.Db.Models
{
    public class Expense
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}