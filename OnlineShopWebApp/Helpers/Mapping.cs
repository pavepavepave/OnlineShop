using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

namespace OnlineShopWebApp.Helpers
{
    public static class Mapping
    {
        public static ProductVM ToProductVM(this Product product)
        {
            var productVMImagePath = product.Images.Select(x => x.Url).ToList();
            
            return new ProductVM
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                ImagePath = productVMImagePath 
            };
        }

        public static List<ProductVM> ToProductsVM(List<Product> products)
        {
            return products.Select(x => ToProductVM(x)).ToList();
        }
        
        public static Product ToProduct(this AddProductVM addProductVM, List<string> imagesPaths)
        {
            return new Product
            {
                Name = addProductVM.Name,
                Cost = addProductVM.Cost,
                Description = addProductVM.Description,
                Images = ToImages(imagesPaths)
            };
        }    
        
        public static EditProductVM ToEditProductVM(this Product product)
        {
            return new EditProductVM
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                ImagePath = product.Images.ToPaths()
            };
        }

        public static Product ToProduct(this EditProductVM editProductVM)
        {
            return new Product
            {
                Id = editProductVM.Id,
                Name = editProductVM.Name,
                Cost = editProductVM.Cost,
                Description = editProductVM.Description,
                Images = editProductVM.ImagePath.ToImages()
            };
        }    
        
        private static List<Image> ToImages(this List<string> paths)
        {
            return paths.Select(x => new Image { Url = x }).ToList();
        }        
        
        private static List<string> ToPaths (this List<Image> paths)
        {
            return paths.Select(x => x.Url ).ToList();
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
                    Product = ToProductVM(x.Product)
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
        
        public static PaintingVM ToPaintingVM(this Painting painting)
        {
            var paintingVMImagePath = painting.Images.Select(x => x.Url).ToList();
            
            return new PaintingVM
            {
                Id = painting.Id,
                Name = painting.Name,
                Description = painting.Description,
                ImagePath = paintingVMImagePath 
            };
        }

        public static List<PaintingVM> ToPaintingsVM(List<Painting> paintings)
        {
            return paintings.Select(x => ToPaintingVM(x)).ToList();
        }
        
        public static Painting ToPainting(this AddPaintingVM addPaintingVM, List<string> imagesPaths)
        {
            return new Painting
            {
                Name = addPaintingVM.Name,
                Description = addPaintingVM.Description,
                Images = ToImagesPainting(imagesPaths)
            };
        }    
        
        public static EditPaintingVM ToEditPaintingVM(this Painting product)
        {
            return new EditPaintingVM
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImagePath = product.Images.ToPaths()
            };
        }

        public static Painting ToPainting(this EditPaintingVM editPaintingVM)
        {
            return new Painting
            {
                Id = editPaintingVM.Id,
                Name = editPaintingVM.Name,
                Description = editPaintingVM.Description,
                Images = editPaintingVM.ImagePath.ToImagesPainting()
            };
        }
        
        private static List<ImagePainting> ToImagesPainting(this List<string> paths)
        {
            return paths.Select(x => new ImagePainting { Url = x }).ToList();
        }        
        
        private static List<string> ToPaths (this List<ImagePainting> paths)
        {
            return paths.Select(x => x.Url ).ToList();
        }

    }
}