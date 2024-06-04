using System.Collections.Generic;
using OnlineShop.Db.Models;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class IncomeExpenseBalanceViewModel
    {
        public List<Income> Incomes { get; set; }
        public List<Expense> Expenses { get; set; }
    }
}