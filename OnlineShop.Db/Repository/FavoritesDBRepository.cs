using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Db.Repository
{
    public class FavoritesDbRepository : IFavoritesRepository
    {
        private readonly DatabaseContext databaseContext;

        public FavoritesDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void Add(string userId, Guid id)
        {
            var existingProduct = databaseContext.Favorites.FirstOrDefault(x => x.UserId == userId && x.Product.Id == id);
            if (existingProduct == null)
            {
                var product = databaseContext.Products.FirstOrDefault(x => x.Id == id);
                databaseContext.Favorites.Add(new FavoriteProduct { UserId = userId, Product = product });
                databaseContext.SaveChanges();
            }
        }

        public List<Product> GetAll(string userId)
        {
            return databaseContext.Favorites.Where(x => x.UserId == userId)
                                                    .Include(x => x.Product)
                                                    .Select(x => x.Product)
                                                    .ToList();
        }

        public void Clear(string userId)
        {
            var userFavoriteProducts = databaseContext.Favorites.Where(x => x.UserId == userId).ToList();
            databaseContext.Favorites.RemoveRange(userFavoriteProducts);
            databaseContext.SaveChanges();
        }

        public void Delete(string userId, Guid productId)
        {
            var removingFavorite = databaseContext.Favorites.FirstOrDefault(x => x.UserId == userId && x.Product.Id == productId);
            databaseContext.Favorites.Remove(removingFavorite);
            databaseContext.SaveChanges();
        }
    }
}