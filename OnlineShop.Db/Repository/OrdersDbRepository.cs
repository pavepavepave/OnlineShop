using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Db.Repository
{
    /// <summary>
    /// Репозиторий для работой с базой данных - корзиной товаров.
    /// </summary>
    public class OrdersDbRepository : IOrdersRepository
    {
        private readonly DatabaseContext _databaseContext;

        public OrdersDbRepository(DatabaseContext databaseContext)
        {
            this._databaseContext = databaseContext;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _databaseContext.Orders.Include(x => x.Items).ThenInclude(x => x.Product).ToListAsync();
        }

        public async Task<Order> GetByOrderIdAsync(int id)
        {
            return await _databaseContext.Orders.Include(x => x.Items).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(Order order)
        {
            await _databaseContext.Orders.AddAsync(order);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task EditAsync(int orderId, int status)
        {
            var order = await GetByOrderIdAsync(orderId);
            if (order != null)
            {
                order.Status = (OrderStatus)status;
            }
            await _databaseContext.SaveChangesAsync();
        }
    }
}