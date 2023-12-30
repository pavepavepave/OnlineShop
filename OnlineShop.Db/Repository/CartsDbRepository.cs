using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<Cart> TryGetByUserIdAsync(string userId)
        {
            return await databaseContext.Carts.Include(x => x.Items).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task AddAsync(Product product, string userId)
        {
            var existingCart = await TryGetByUserIdAsync(userId);
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
                await databaseContext.Carts.AddAsync(newCart);
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
            await databaseContext.SaveChangesAsync();
        }

        public async Task ClearAsync(string userId)
        {
            var cart = await TryGetByUserIdAsync(userId);
            cart.Items.Clear();
            await databaseContext.SaveChangesAsync();
        }

        public async Task ReduceAmountAsync(Guid productId, string userId)
        {
            var existingCart = await TryGetByUserIdAsync(userId);
            var existingCartItem = existingCart.Items.FirstOrDefault(x => x.Product.Id == productId);
            if (existingCartItem?.Amount > 1)
            {
                existingCartItem.Amount--;
            }
            else if (existingCartItem?.Amount == 1)
            {
                existingCart.Items.Remove(existingCartItem);
            }
            await databaseContext.SaveChangesAsync();
        }

        public async Task IncreaseAmountAsync(Guid productId, string userId)
        {
            var existingCart = await TryGetByUserIdAsync(userId);
            var existingCartItem = existingCart.Items.FirstOrDefault(x => x.Product.Id == productId);
            if (existingCartItem != null)
            {
                existingCartItem.Amount++;
            }
            await databaseContext.SaveChangesAsync();
        }
    }
}