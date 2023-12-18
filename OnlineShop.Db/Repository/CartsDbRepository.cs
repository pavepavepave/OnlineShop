using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Db.Repository
{
    /// <summary>
    /// Репозиторий для работой с базой данных - корзиной товаров.
    /// </summary>
    public class CartsDbRepository : ICartsRepository
    {
        private readonly DatabaseContext databaseContext;

        public CartsDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Cart TryGetByUserId(string userId)
        {
            return databaseContext.Carts.Include(x => x.Items).ThenInclude(x => x.Product).FirstOrDefault(x => x.UserId == userId);
        }

        public void Add(Product product, string userId)
        {
            var existingCart = TryGetByUserId(userId);
            if (existingCart == null)
            {
                var newCart = new Cart
                {
                    UserId = userId
                };
                newCart.Items = new List<CartItem>
                    {
                        new CartItem
                        {
                            Amount = 1,
                            Product = product,
                            Cart = newCart
                        }
                    };
                databaseContext.Carts.Add(newCart);
            }
            else
            {
                var existingCartItem = existingCart.Items.FirstOrDefault(x => x.Product.Id == product.Id);
                if (existingCartItem != null)
                {
                    existingCartItem.Amount++;
                }
                else
                {
                    existingCart.Items.Add(new CartItem
                    {
                        Amount = 1,
                        Product = product,
                        Cart = existingCart
                    });
                }
            }
            databaseContext.SaveChanges();
        }

        public void Clear(string userId)
        {
            TryGetByUserId(userId).Items.Clear();
            databaseContext.SaveChanges();
        }

        public void ReduceAmount(Guid productId, string userId)
        {
            var existingCart = TryGetByUserId(userId);
            var existingCartItem = existingCart.Items.FirstOrDefault(x => x.Product.Id == productId);
            if (existingCartItem?.Amount > 1)
            {
                existingCartItem.Amount--;
            }
            else if (existingCartItem?.Amount == 1)
            {
                existingCart.Items.Remove(existingCartItem);
            }
            databaseContext.SaveChanges();
        }

        public void IncreaseAmount(Guid productId, string userId)
        {
            var existingCart = TryGetByUserId(userId);
            var existingCartItem = existingCart.Items.FirstOrDefault(x => x.Product.Id == productId);
            if (existingCartItem != null)
            {
                existingCartItem.Amount++;
            }
            databaseContext.SaveChanges();
        }
    }
}