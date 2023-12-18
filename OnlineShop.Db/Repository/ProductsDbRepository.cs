using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Db.Repository
{
    public class ProductsDbRepository : IProductsRepository
    {
        private readonly DatabaseContext databaseContext;

        public ProductsDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public List<Product> GetAll()
        {
            return databaseContext.Products.ToList();
        }

        public Product TryGetById(Guid id)
        {
            return databaseContext.Products.FirstOrDefault(product => product.Id == id);
        }

        public void Delete(Guid id)
        {
            var product = TryGetById(id);
            if (product == null)
            {
                return;
            }
            databaseContext.Products.Remove(product);
            databaseContext.SaveChanges();
        }

        public void Add(Product product)
        {
            databaseContext.Products.Add(product);
            databaseContext.SaveChanges();
        }

        public void Edit(Product product)
        {
            var existingProduct = TryGetById(product.Id);
            if (existingProduct == null)
            {
                return;
            }
            existingProduct.Name = product.Name;
            existingProduct.Cost = product.Cost;
            existingProduct.Description = product.Description;
            databaseContext.SaveChanges();
        }
    }
}