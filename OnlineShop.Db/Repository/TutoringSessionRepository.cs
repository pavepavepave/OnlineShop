using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Db.Repository
{
    public class TutoringSessionRepository : ITutoringSessionRepository
    {
        private readonly DatabaseContext _context;

        public TutoringSessionRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TutoringSession>> GetAllAsync()
        {
            return await _context.TutoringSessions.Include(ts => ts.TimeSlot).ToListAsync();
        }

        public async Task<TutoringSession> GetByIdAsync(Guid id)
        {
            return await _context.TutoringSessions.Include(ts => ts.TimeSlot).FirstOrDefaultAsync(ts => ts.Id == id);
        }

        public async Task AddAsync(TutoringSession session)
        {
            await _context.TutoringSessions.AddAsync(session);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TutoringSession session)
        {
            _context.TutoringSessions.Update(session);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var session = await GetByIdAsync(id);
            if (session != null)
            {
                _context.TutoringSessions.Remove(session);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(DateTime date, int timeSlotId, string serviceType)
        {
            return await _context.TutoringSessions.AnyAsync(ts => ts.Date == date && ts.TimeSlotId == timeSlotId && ts.ServiceType == serviceType);
        }
    }
}