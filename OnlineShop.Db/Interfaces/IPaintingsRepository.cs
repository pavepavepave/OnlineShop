using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineShop.Db.Interfaces
{
    public interface IPaintingsRepository
    {
        Task<List<Painting>> GetAllAsync();
        Task<Painting> TryGetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task AddAsync(Painting painting);
        Task EditAsync(Painting painting, IFormFile[] uploadedFiles);
    }
}