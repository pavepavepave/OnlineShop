using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Db.Repository
{
    public class ProductsDbRepository : IProductsRepository
    {
        private readonly DatabaseContext databaseContext;

        public ProductsDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await databaseContext.Products.ToListAsync();
        }

        public async Task<Product> TryGetByIdAsync(Guid id)
        {
            return await databaseContext.Products.FirstOrDefaultAsync(product => product.Id == id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await TryGetByIdAsync(id);
            if (product == null)
            {
                return;
            }
            databaseContext.Products.Remove(product);
            await databaseContext.SaveChangesAsync();
        }

        public async Task AddAsync(Product product)
        {
            databaseContext.Products.Add(product);
            await databaseContext.SaveChangesAsync();
        }

        public async Task EditAsync(Product product)
        {
            var existingProduct = await TryGetByIdAsync(product.Id);
            if (existingProduct == null)
            {
                return;
            }
            existingProduct.Name = product.Name;
            existingProduct.Cost = product.Cost;
            existingProduct.Description = product.Description;
            await databaseContext.SaveChangesAsync();
        }
    }
}