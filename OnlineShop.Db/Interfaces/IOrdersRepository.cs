using OnlineShop.Db.Models;
using System.Collections.Generic;

namespace OnlineShop.Db.Interfaces
{
    public interface IOrdersRepository
    {
        List<Order> GetAll();
        Order GetByOrderId(int id);
        void Create(Order order);
        void Edit(int id, int status);
    }
}