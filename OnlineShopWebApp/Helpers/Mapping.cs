using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.Helpers
{
    public static class Mapping
    {
        public static ProductVM ToProductViewModel(Product product)
        {
            return new ProductVM
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                Image = product.Image
            };
        }
        public static List<ProductVM> ToProductViewModels(List<Product> products)
        {
            return products.Select(x => ToProductViewModel(x)).ToList();
        }

        public static CartVM ToCartViewModel(Cart cart)
        {
            if (cart == null)
            {
                return null;
            }
            return new CartVM
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Items = ToCartViewModels(cart.Items)
            };
        }

        public static List<CartItemVM> ToCartViewModels(List<CartItem> cartDbItems)
        {
            return cartDbItems.Select(x =>
                new CartItemVM
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    Product = ToProductViewModel(x.Product)
                })
                .ToList();
        }

        public static OrderVM ToOrderViewModel(Order order)
        {
            if (order == null)
            {
                return null;
            }
            return new OrderVM
            {
                Id = order.Id,
                UserId = order.UserId,
                Items = ToCartViewModels(order.Items),
                Status = (OrderStatusVM)(int)order.Status,
                DateOrder = order.DateOrder,
                FirstName = order.FirstName,
                SecondName = order.SecondName,
                Address = order.Address,
                Phone = order.Phone,
                Email = order.Email
            };
        }

        public static List<OrderVM> ToOrderViewModels(List<Order> orderDbItems)
        {
            return orderDbItems.Select(x => ToOrderViewModel(x)).ToList();
        }

        public static Order ToOrder(OrderVM orderVM, Cart existingCart, string userId)
        {
            var order = new Order
            {
                Items = existingCart.Items.ToList(),
                UserId = userId,
                FirstName = orderVM.FirstName,
                SecondName = orderVM.SecondName,
                Address = orderVM.Address,
                Phone = orderVM.Phone,
                Email = orderVM.Email
            };
            return order;
        }

        public static UserVM ToUserViewModel(this User user)
        {
            return new UserVM
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                Email = user.Email,
                DateCreate = user.DateCreate
            };
        }

        public static UserVM ToUserViewModel(this AccountVM user)
        {
            return new UserVM
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                Email = user.Email,
            };
        }

        public static EditUserVM ToEditUserViewModel(this User user)
        {
            return new EditUserVM
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                Email = user.Email
            };
        }

        public static ChangePasswordVM ToChangePasswordViewModel(this User user)
        {
            return new ChangePasswordVM
            {
                Id = user.Id,
                UserName = user.UserName
            };
        }
    }
}