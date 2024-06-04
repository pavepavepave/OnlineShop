using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Repository
{
    public class PaintingOrderRepository : IPaintingOrderRepository
    {
        private readonly DatabaseContext _databaseContext;

        public PaintingOrderRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        
        public async Task<List<PaintingOrder>> GetAllAsync()
        {
            return await _databaseContext.PaintingOrder.ToListAsync();
        }

        public async Task<PaintingOrder> GetByPaintingOrderIdAsync(int id)
        {
            return await _databaseContext.PaintingOrder.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(PaintingOrder paintingOrder)
        {
            await _databaseContext.PaintingOrder.AddAsync(paintingOrder);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task EditPaintingOrderAsync(int orderId, PaintingOrder paintingOrder, int status)
        {
            var order = await GetByPaintingOrderIdAsync(orderId);
            if (order != null)
            {
                order.Status = (OrderStatus)status;
                order.Description = paintingOrder.Description;
                order.AdditionalRequest = paintingOrder.AdditionalRequest;
                order.Cost = paintingOrder.Cost;
            }
            await _databaseContext.SaveChangesAsync();
        }
    }
}