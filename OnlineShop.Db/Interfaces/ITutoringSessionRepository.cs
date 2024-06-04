using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface ITutoringSessionRepository
    {
        Task<IEnumerable<TutoringSession>> GetAllAsync();
        Task<TutoringSession> GetByIdAsync(Guid id);
        Task AddAsync(TutoringSession session);
        Task UpdateAsync(TutoringSession session);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(DateTime date, int timeSlotId, string serviceType);
    }
}