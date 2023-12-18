using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Db.Repository
{
    public class CompareDbRepository : ICompareRepository
    {
        private readonly DatabaseContext databaseContext;

        public CompareDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void Add(string userId, Guid id)
        {
            var existingProduct = databaseContext.Compares.FirstOrDefault(x => x.UserId == userId && x.Product.Id == id);
            if (existingProduct == null && CheckCount(userId))
            {
                var product = databaseContext.Products.FirstOrDefault(x => x.Id == id);
                databaseContext.Compares.Add(new CompareProduct { UserId = userId, Product = product });
                databaseContext.SaveChanges();
            }
        }

        public List<Product> GetAll(string userId)
        {
            return databaseContext.Compares.Where(x => x.UserId == userId)
                                                    .Include(x => x.Product)
                                                    .Select(x => x.Product)
                                                    .ToList();
        }

        public void Clear(string userId)
        {
            var favorites = databaseContext.Compares.Where(x => x.UserId == userId).ToList();
            databaseContext.Compares.RemoveRange(favorites);
            databaseContext.SaveChanges();
        }

        public void Delete(string userId, Guid productId)
        {
            var removingFavorite = databaseContext.Compares.FirstOrDefault(x => x.UserId == userId && x.Product.Id == productId);
            databaseContext.Compares.Remove(removingFavorite);
            databaseContext.SaveChanges();
        }
        public bool CheckCount(string userId)
        {
            return databaseContext.Compares.Count(x => x.UserId == userId) < 4;
        }
    }
}