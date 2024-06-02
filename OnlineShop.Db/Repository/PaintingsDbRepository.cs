using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineShop.Db.Repository
{
    public class PaintingsDbRepository : IPaintingsRepository
    {
        private readonly DatabaseContext _databaseContext;

        public PaintingsDbRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Painting>> GetAllAsync()
        {
            return await _databaseContext.Paintings.Include(x=>x.Images).ToListAsync();
        }

        public async Task<Painting> TryGetByIdAsync(Guid id)
        {
            return await _databaseContext.Paintings.Include(x => x.Images)
                              .FirstOrDefaultAsync(painting => painting.Id == id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var painting = await TryGetByIdAsync(id);
            if (painting == null)
            {
                return;
            }
            _databaseContext.Paintings.Remove(painting);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task AddAsync(Painting painting)
        {
            _databaseContext.Paintings.Add(painting);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task EditAsync(Painting painting, IFormFile[] uploadedFiles)
        {
            var currentPainting = await TryGetByIdAsync(painting.Id);
            currentPainting.Name = painting.Name;
            currentPainting.Description = painting.Description;
    
            if(uploadedFiles != null)
            {
                foreach (var image in currentPainting.Images)
                {
                    _databaseContext.ImagesPaintings.Remove(image);
                }

                foreach (var image in painting.Images)
                {
                    image.PaintingId = painting.Id;
                    _databaseContext.ImagesPaintings.Add(image);
                }
            }          

            await _databaseContext.SaveChangesAsync();
        }
    }
}