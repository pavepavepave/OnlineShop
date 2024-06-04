using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OnlineShop.Db;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class FinanceController : Controller
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;

        public FinanceController(IIncomeRepository incomeRepository, IExpenseRepository expenseRepository)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
        }

        public async Task<IActionResult> Incomes()
        {
            var incomes = await _incomeRepository.GetAllIncomesAsync();
            return View(incomes.OrderByDescending(i => i.Date).ToList());
        }

        public async Task<IActionResult> Expenses()
        {
            var expenses = await _expenseRepository.GetAllExpensesAsync();
            return View(expenses.OrderByDescending(e => e.Date).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> GetIncomeById(Guid id)
        {
            var income = await _incomeRepository.GetIncomeByIdAsync(id);
            if (income == null) return NotFound();
            return Json(income);
        }

        [HttpGet]
        public async Task<IActionResult> GetExpenseById(Guid id)
        {
            var expense = await _expenseRepository.GetExpenseByIdAsync(id);
            if (expense == null) return NotFound();
            return Json(expense);
        }

        [HttpPost]
        public async Task<IActionResult> EditIncome(Income income)
        {
            if (ModelState.IsValid)
            {
                await _incomeRepository.UpdateIncomeAsync(income);
                var incomes = await _incomeRepository.GetAllIncomesAsync();
                return Json(incomes.OrderByDescending(i => i.Date).ToList());
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> EditExpense(Expense expense)
        {
            if (ModelState.IsValid)
            {
                await _expenseRepository.UpdateExpenseAsync(expense);
                var expenses = await _expenseRepository.GetAllExpensesAsync();
                return Json(expenses.OrderByDescending(e => e.Date).ToList());
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteIncome(Guid id)
        {
            await _incomeRepository.DeleteIncomeAsync(id);
            var incomes = await _incomeRepository.GetAllIncomesAsync();
            return Json(incomes.OrderByDescending(i => i.Date).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteExpense(Guid id)
        {
            await _expenseRepository.DeleteExpenseAsync(id);
            var expenses = await _expenseRepository.GetAllExpensesAsync();
            return Json(expenses.OrderByDescending(e => e.Date).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> AddIncome(Income income)
        {
            if (ModelState.IsValid)
            {
                await _incomeRepository.AddIncomeAsync(income);
                var incomes = await _incomeRepository.GetAllIncomesAsync();
                return Json(incomes.OrderByDescending(i => i.Date).ToList());
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> AddExpense(Expense expense)
        {
            if (ModelState.IsValid)
            {
                await _expenseRepository.AddExpenseAsync(expense);
                var expenses = await _expenseRepository.GetAllExpensesAsync();
                return Json(expenses.OrderByDescending(e => e.Date).ToList());
            }
            return Json(new { success = false });
        }
    }
}