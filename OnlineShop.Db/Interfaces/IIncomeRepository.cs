using System;
using OnlineShop.Db.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Db.Interfaces
{
    public interface IIncomeRepository
    {
        Task<List<Income>> GetAllIncomesAsync();
        Task AddIncomeAsync(Income income);
        Task UpdateIncomeAsync(Income income);
        Task DeleteIncomeAsync(Guid id);
        Task<Income> GetIncomeByIdAsync(Guid id);
    }
}