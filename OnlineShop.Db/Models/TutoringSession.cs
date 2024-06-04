using System;

namespace OnlineShop.Db.Models
{
    public class TutoringSession
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public int TimeSlotId { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public DateTime Date { get; set; } // Дата записи
        public Guid BookingId { get; set; } = Guid.NewGuid(); // Идентификатор для проверки записи
        public string ServiceType { get; set; } // Тип услуги
    }
}