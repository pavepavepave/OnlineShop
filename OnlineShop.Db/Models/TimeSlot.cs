using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class TimeSlot
    {
        public int Id { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsAvailable { get; set; } = true;
        public List<TutoringSession> TutoringSessions { get; set; }
    }
}