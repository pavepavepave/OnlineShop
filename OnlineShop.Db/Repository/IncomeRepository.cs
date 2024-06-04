using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repository
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly DatabaseContext _context;

        public IncomeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Income>> GetAllIncomesAsync()
        {
            return await _context.Incomes.ToListAsync();
        }

        public async Task AddIncomeAsync(Income income)
        {
            _context.Incomes.Add(income);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateIncomeAsync(Income income)
        {
            _context.Incomes.Update(income);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIncomeAsync(Guid id)
        {
            var income = await GetIncomeByIdAsync(id);
            if (income != null)
            {
                _context.Incomes.Remove(income);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Income> GetIncomeByIdAsync(Guid id)
        {
            return await _context.Incomes.FindAsync(id);
        }
    }
}