using System;
using OnlineShop.Db.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Db.Interfaces
{
    public interface IExpenseRepository
    {
        Task<List<Expense>> GetAllExpensesAsync();
        Task AddExpenseAsync(Expense expense);
        Task UpdateExpenseAsync(Expense expense);
        Task DeleteExpenseAsync(Guid id);
        Task<Expense> GetExpenseByIdAsync(Guid id);
    }
}