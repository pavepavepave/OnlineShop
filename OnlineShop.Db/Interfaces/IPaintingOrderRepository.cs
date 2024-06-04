using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IPaintingOrderRepository
    {
        Task<List<PaintingOrder>> GetAllAsync();
        Task<PaintingOrder> GetByPaintingOrderIdAsync(int id);
        Task CreateAsync(PaintingOrder paintingOrder);
        Task EditPaintingOrderAsync(int id, PaintingOrder paintingOrder, int status);
    }
}