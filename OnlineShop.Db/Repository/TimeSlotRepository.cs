using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Db.Repository
{
    public class TimeSlotRepository : ITimeSlotRepository
    {
        private readonly DatabaseContext _context;

        public TimeSlotRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TimeSlot>> GetAvailableTimeSlotsAsync(DateTime date, string serviceType)
        {
            var bookedTimeSlots = await _context.TutoringSessions
                .Where(ts => ts.Date == date && ts.ServiceType == serviceType)
                .Select(ts => ts.TimeSlotId)
                .ToListAsync();

            return await _context.TimeSlots
                .Where(ts => !bookedTimeSlots.Contains(ts.Id))
                .ToListAsync();
        }
    }
}