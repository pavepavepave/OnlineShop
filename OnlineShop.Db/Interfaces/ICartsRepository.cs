using OnlineShop.Db.Models;
using System;

namespace OnlineShop.Db.Interfaces
{
    public interface ICartsRepository
    {
        Cart TryGetByUserId(string userId);
        void Add(Product product, string userId);
        void Clear(string userId);
        void ReduceAmount(Guid productId, string userId);
        void IncreaseAmount(Guid productId, string userId);
    }
}