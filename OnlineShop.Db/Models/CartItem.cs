namespace OnlineShop.Db.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public Cart Cart { get; set; }
        public int Amount { get; set; }
    }
}