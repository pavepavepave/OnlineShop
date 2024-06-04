using System;
using OnlineShop.Db.Models;

namespace OnlineShopWebApp.Models
{
    public class PaintingOrderVM
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateOrder { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string AdditionalContact { get; set; } // Дополнительный способ связи
        public string PaintingSize { get; set; } // Размер картины
        public string PaintingTheme { get; set; } // Тема картины
        public string ColorPalette { get; set; } // Цветовая гамма
        public string Description { get; set; } // Описание/пожелания
        public string AdditionalRequest { get; set; } // Дополнительное пожелание
        public decimal Cost { get; set; }
    }
}