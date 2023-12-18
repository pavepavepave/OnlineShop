using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Db.Repository
{
    /// <summary>
    /// Репозиторий для работой с базой данных - корзиной товаров.
    /// </summary>
    public class OrdersDbRepository : IOrdersRepository
    {
        private readonly DatabaseContext databaseContext;

        public OrdersDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public List<Order> GetAll()
        {
            return databaseContext.Orders.Include(x => x.Items).ThenInclude(x => x.Product).ToList();
        }

        public Order GetByOrderId(int id)
        {
            return databaseContext.Orders.Include(x => x.Items).ThenInclude(x => x.Product).FirstOrDefault(x => x.Id == id);
        }

        public void Create(Order order)
        {
            databaseContext.Orders.Add(order);
            databaseContext.SaveChanges();
        }

        public void Edit(int orderId, int status)
        {
            var order = GetByOrderId(orderId);
            if (order != null)
            {
                order.Status = (OrderStatus)status;
            }
            databaseContext.SaveChanges();
        }
    }
}