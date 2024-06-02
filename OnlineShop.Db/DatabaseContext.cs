using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;

namespace OnlineShop.Db
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Painting> Paintings { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<ImagePainting> ImagesPaintings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .Property(p => p.Cost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Image>().HasOne(p => p.Product).WithMany(p => p.Images)
                .HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Cascade);

            var productId1 = Guid.Parse("ac80a1ac-4c7b-4662-8aac-f14a5a90b957");
            var productId2 = Guid.Parse("0122481d-10e3-4b58-89ee-c72269d5c95e");
            var productId3 = Guid.Parse("c60de567-a236-4f59-bc76-4aa1109545d6");
            var productId4 = Guid.Parse("37acd775-ee3a-4e6d-83c2-3458adcbf87f");
            var productId5 = Guid.Parse("21b68580-8f57-45a4-a415-1ccfa97337c3");
            var productId6 = Guid.Parse("0bc0cbd7-de40-46bd-8133-59c8a35b2761");

            var image1 = new Image
            {
                Id = Guid.Parse("56509768-29a5-4bdf-ada5-66e40a06b2b7"),
                Url = "/images/goods/image1.jpg",
                ProductId = productId1
            };
            var image2 = new Image
            {
                Id = Guid.Parse("d096d5aa-159a-442c-8507-e4449cb78376"),
                Url = "/images/goods/image2.jpg",
                ProductId = productId2
            };
            var image3 = new Image
            {
                Id = Guid.Parse("d4571918-a7ec-4361-ba2b-8b04ec88ad5b"),
                Url = "/images/goods/image3.jpg",
                ProductId = productId3
            };
            var image4 = new Image
            {
                Id = Guid.Parse("fd4ad9ce-2b57-4abb-9330-6e7ca265b688"),
                Url = "/images/goods/image4.jpg",
                ProductId = productId4
            };
            var image5 = new Image
            {
                Id = Guid.Parse("d720a522-4957-4a50-af7a-8edee651f53c"),
                Url = "/images/goods/image5.jpg",
                ProductId = productId5
            };
            var image6 = new Image
            {
                Id = Guid.Parse("99a13c69-060e-4663-a6a4-8490630bd027"),
                Url = "/images/goods/image6.jpg",
                ProductId = productId6
            };

            modelBuilder.Entity<Image>().HasData(image1, image2, image3, image4, image5, image6);

            modelBuilder.Entity<Product>()
                .HasData(
                    new Product
                    {
                        Id = productId1,
                        Name = "Гуашь художественная, набор из 5 цветов",
                        Cost = 400,
                        Description = "Набор гуаши 5 цветов, художественная 40 мл."
                    },
                    new Product
                    {
                        Id = productId2,
                        Name = "Краска акварель",
                        Cost = 810,
                        Description = "Краски акварельные для рисования 24 цвета."
                    },
                    new Product
                    {
                        Id = productId3,
                        Name = "Краски акриловые, 5х50 мл",
                        Cost = 2000,
                        Description = "5 цветов, 50 мл."
                    },
                    new Product
                    {
                        Id = productId4,
                        Name = "Набор масляных красок, 10 цветов",
                        Cost = 1950,
                        Description = "Краски масляные художественные для рисования, набор из 10 цветов."
                    },
                    new Product
                    {
                        Id = productId5,
                        Name = "Набор сухой пастели, 24 цвета",
                        Cost = 750,
                        Description = "Набор пастели 24 цвета."
                    },
                    new Product
                    {
                        Id = productId6,
                        Name = "Набор меловой пастели",
                        Cost = 910,
                        Description = "Набор меловой пастели 48 цв."
                    });

            #region PicturesForPortfolio

            modelBuilder.Entity<ImagePainting>().HasOne(p => p.Painting).WithMany(p => p.Images)
                .HasForeignKey(p => p.PaintingId).OnDelete(DeleteBehavior.Cascade);

            var paintingId1 = Guid.Parse("01e5de59-7e9d-4985-9fb9-84e67fbbbdfe");
            var paintingId2 = Guid.Parse("dc00811d-5c83-4bff-ab04-e10f56dc4faf");
            var paintingId3 = Guid.Parse("2bee2bc7-3a2b-49ed-a608-c24d2d3eecec");
            var paintingId4 = Guid.Parse("158cbcb3-cd2b-4a27-bafd-e1c026e97dc0");
            var paintingId5 = Guid.Parse("fb352635-60db-40cf-8f87-6a94a1355e49");
            var paintingId6 = Guid.Parse("2d6334d7-8965-450e-8c37-741031080e31");

            var imagePainting1 = new ImagePainting
            {
                Id = Guid.Parse("26a8e059-69bc-47ac-93fd-7f75715d8f82"),
                Url = "/images/paintings/image1.jpg",
                PaintingId = paintingId1
            };
            var imagePainting2 = new ImagePainting
            {
                Id = Guid.Parse("8e913665-4107-40c2-bc31-be6bc537d14d"),
                Url = "/images/paintings/image2.jpg",
                PaintingId = paintingId2
            };
            var imagePainting3 = new ImagePainting
            {
                Id = Guid.Parse("2fcbeb71-e0bd-49dd-9643-5b7c6b07b993"),
                Url = "/images/paintings/image3.jpg",
                PaintingId = paintingId3
            };
            var imagePainting4 = new ImagePainting
            {
                Id = Guid.Parse("bb9e2489-d497-4113-8fd3-1d9398524d04"),
                Url = "/images/paintings/image4.jpg",
                PaintingId = paintingId4
            };
            var imagePainting5 = new ImagePainting
            {
                Id = Guid.Parse("89152783-b7eb-4ee1-be08-d2b1e44ae58d"),
                Url = "/images/paintings/image5.jpg",
                PaintingId = paintingId5
            };
            var imagePainting6 = new ImagePainting
            {
                Id = Guid.Parse("86ace72f-e08c-42ab-8fb6-13abb11ea178"),
                Url = "/images/paintings/image6.jpg",
                PaintingId = paintingId6
            };

            modelBuilder.Entity<ImagePainting>().HasData(
                imagePainting1, imagePainting2, imagePainting3, imagePainting4, imagePainting5, imagePainting6
            );

            modelBuilder.Entity<Painting>().HasData(
                new Painting
                {
                    Id = paintingId1,
                    Name = "Пряный аромат",
                    Description = "Холст. Масло. 20х25 см. 2022 г."
                },
                new Painting
                {
                    Id = paintingId2,
                    Name = "Северное сияние",
                    Description = "Холст. Масло. 40х60 см. 2021 г."
                },
                new Painting
                {
                    Id = paintingId3,
                    Name = "Городская эстетика",
                    Description = "Холст. Масло. 20х25 см. 2023 г."
                },
                new Painting
                {
                    Id = paintingId4,
                    Name = "Балерина",
                    Description = "Холст. Масло. 20х25 см. 2023 г."
                },
                new Painting
                {
                    Id = paintingId5,
                    Name = "Важный матч",
                    Description = "Холст. Масло. 50х60. 2021 г."
                },
                new Painting
                {
                    Id = paintingId6,
                    Name = "Зимний вечер",
                    Description = "Холст. Масло. 40х60 см. 2022 г."
                });

            #endregion
        }
    }
}