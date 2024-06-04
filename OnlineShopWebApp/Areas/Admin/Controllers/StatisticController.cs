using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopWebApp.Areas.Admin.Models;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatisticController : Controller
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;

        public StatisticController(IIncomeRepository incomeRepository, IExpenseRepository expenseRepository)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
        }

        public async Task<IActionResult> IncomeStatistics()
        {
            var incomes = await _incomeRepository.GetAllIncomesAsync();
            return View(incomes.OrderByDescending(i => i.Date).ToList());
        }

        public async Task<IActionResult> ExpenseStatistics()
        {
            var expenses = await _expenseRepository.GetAllExpensesAsync();
            return View(expenses.OrderByDescending(e => e.Date).ToList());
        }
        
        public async Task<IActionResult> IncomeExpenseBalance()
        {
            var incomes = await _incomeRepository.GetAllIncomesAsync();
            var expenses = await _expenseRepository.GetAllExpensesAsync();
            var balanceData = new IncomeExpenseBalanceViewModel
            {
                Incomes = incomes.OrderByDescending(i => i.Date).ToList(),
                Expenses = expenses.OrderByDescending(e => e.Date).ToList()
            };
            return View(balanceData);
        }
    }
}