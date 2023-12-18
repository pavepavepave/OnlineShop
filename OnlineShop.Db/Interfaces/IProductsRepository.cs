using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Interfaces
{
    public interface IProductsRepository
    {
        List<Product> GetAll();
        Product TryGetById(Guid id);
        void Delete(Guid id);
        void Add(Product product);
        void Edit(Product product);
    }
}