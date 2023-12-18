using OnlineShopWebApp.Areas.Admin.Models;

namespace OnlineShopWebApp.Models
{
    /// <summary>
    /// Одна отдельная позиция товара в корзине.
    /// </summary>
    public class CartItemVM
    {
        public int Id { get; set; }
        public ProductVM Product { get; set; }
        public int Amount { get; set; }
        public decimal Cost
        {
            get
            {
                return Product.Cost * Amount;
            }
        }
    }
}