using OnlineShop.Db.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Db.Interfaces
{
    public interface IOrdersRepository
    {
        Task<List<Order>> GetAllAsync();
        Task<Order> GetByOrderIdAsync(int id);
        Task CreateAsync(Order order);
        Task EditAsync(int id, int status);
    }
}