using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
            return await databaseContext.Products.Include(x=>x.Images).ToListAsync();
        }

        public async Task<Product> TryGetByIdAsync(Guid id)
        {
            return await databaseContext.Products.Include(x => x.Images)
                              .FirstOrDefaultAsync(product => product.Id == id);
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

        public async Task EditAsync(Product product, IFormFile[] uploadedFiles)
        {
            var currentProduct = await TryGetByIdAsync(product.Id);
            currentProduct.Name = product.Name;
            currentProduct.Cost = product.Cost;
            currentProduct.Description = product.Description;
    
            if(uploadedFiles != null)
            {
                foreach (var image in currentProduct.Images)
                {
                    databaseContext.Images.Remove(image);
                }

                foreach (var image in product.Images)
                {
                    image.ProductId = product.Id;
                    databaseContext.Images.Add(image);
                }
            }          

            await databaseContext.SaveChangesAsync();
        }
    }
}