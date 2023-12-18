using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Db.Models
{
    public enum OrderStatus
    {
        [Display(Name = "Создано")]
        Created,
        [Display(Name = "Принято")]
        Accepted,
        [Display(Name = "Оплачено")]
        Paid,
        [Display(Name = "Доставляется")]
        Delivering,
        [Display(Name = "Доставлено")]
        Delivered,
        [Display(Name = "Исполнено")]
        Comlpleted,
        [Display(Name = "Отменено")]
        Canceled
    }
}