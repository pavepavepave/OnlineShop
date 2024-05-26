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
        
        public DbSet<Cart> Carts { get; set; }
        
        public DbSet<CartItem> CartItems { get; set; }
        
        public DbSet<FavoriteProduct> Favorites { get; set; }
        
        public DbSet<CompareProduct> Compares { get; set; }
        
        public DbSet<Order> Orders { get; set; }
        
        public DbSet<Image> Images { get; set; }

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
        }
    }
}