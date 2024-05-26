using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineShop.Db.Interfaces
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> TryGetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task AddAsync(Product product);
        Task EditAsync(Product product, IFormFile[] uploadedFiles);
    }
}