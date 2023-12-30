using OnlineShop.Db.Models;
using System;
using System.Threading.Tasks;

namespace OnlineShop.Db.Interfaces
{
    public interface ICartsRepository
    {
        Task<Cart> TryGetByUserIdAsync(string userId);
        Task AddAsync(Product product, string userId);
        Task ClearAsync(string userId);
        Task ReduceAmountAsync(Guid productId, string userId);
        Task IncreaseAmountAsync(Guid productId, string userId);
    }
}