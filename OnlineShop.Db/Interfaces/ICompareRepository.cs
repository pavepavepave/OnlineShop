using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Interfaces
{
    public interface ICompareRepository
    {
        void Add(string userId, Guid id);
        List<Product> GetAll(string userId);
        void Clear(string userId);
        void Delete(string userId, Guid id);
    }
}