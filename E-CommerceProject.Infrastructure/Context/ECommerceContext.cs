﻿using E_CommerceProject.Models;
using E_CommerceProject.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProject.Infrastructure.Context
{
    public class ECommerceContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<UserCart> UserCarts { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Size> Sizes { get; set; }

        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Brand>(builder =>
            {
                builder.Property(c => c.Name)
                       .IsRequired()
                       .HasMaxLength(50);
            });

            modelBuilder.Entity<Order>(builder =>
            {
                builder.Property(c => c.OrderStatus)
                       .IsRequired();

                builder.Property(c => c.OrderDate)
                       .IsRequired();
            });

            modelBuilder.Entity<OrderDetails>(builder =>
            {
                builder.HasKey(c => new { c.OrderId, c.ProductId });

                builder.Property(c => c.Quantity)
                       .IsRequired();

                builder.HasOne(c => c.Product)
                       .WithMany(c => c.OrderDetails)
                       .HasForeignKey(c => c.ProductId);

                builder.HasOne(c => c.Order)
                       .WithMany(c => c.OrdersDetails)
                       .HasForeignKey(c => c.OrderId);

            });

            modelBuilder.Entity<Product>(builder =>
            {
                builder.Property(c => c.Name)
                       .IsRequired()
                       .HasMaxLength(200);

                builder.Property(c => c.Price)
                       .IsRequired();

                builder.Property(c => c.Description)
                       .HasMaxLength(500)
                       .IsRequired();

                builder.HasOne(c => c.Brand)
                       .WithMany(c => c.Products)
                       .HasForeignKey(c => c.BrandId);
            });

            modelBuilder.Entity<Review>(builder =>
            {
                builder.HasKey(c => new { c.UserId, c.ProductId});

                builder.HasOne(c => c.User)
                       .WithMany(c => c.Reviews)
                       .HasForeignKey(c => c.UserId)
                       .OnDelete(DeleteBehavior.NoAction);

                builder.HasOne(c => c.Product)
                       .WithMany(c => c.Reviews)
                       .HasForeignKey(c => c.ProductId)
                       .OnDelete(DeleteBehavior.NoAction);

            });

            modelBuilder.Entity<User>(builder =>
            {
                builder.Property(c => c.Name)
                       .IsRequired()
                       .HasMaxLength(100);

                builder.Property(c => c.Email)
                       .IsRequired()
                       .HasMaxLength(200);

                builder.Property(c => c.Role)
                       .IsRequired();
                builder.HasOne(c => c.Cart)
                        .WithOne(c => c.User)
                        .HasForeignKey<UserCart>(c => c.UserId);
            });


            modelBuilder.Entity<UserCart>(builder =>
            {
                builder.HasKey(c => new { c.UserId, c.ProductId });

                builder.Property(c => c.Quantity)
                       .IsRequired();

                builder.HasOne(c => c.Product)
                       .WithMany(c => c.UserCarts)
                       .HasForeignKey(c => c.ProductId);
            });

            modelBuilder.Entity<WishList>(builder =>
            {
                builder.HasKey(c => new { c.UserId, c.ProductId });
            });

            modelBuilder.Entity<Contact>(builder =>
            {
                builder.Property(c =>c.Name)
                       .IsRequired()
                       .HasMaxLength(100);

                builder.Property(c =>c.Email)
                       .IsRequired()
                       .HasMaxLength(100);

                builder.Property(c =>c.Subject)
                       .IsRequired()
                       .HasMaxLength(100);

                builder.Property(c =>c.Message)
                       .IsRequired()
                       .HasMaxLength(500);
            });
            List<Category> categories = new List<Category>() { new Category() { Id =1, Name= "Man"},
                                                               new Category() { Id =2, Name= "Woman"},
                                                               new Category() { Id =3, Name= "Kids"}};


            modelBuilder.Entity<Category>()
            .HasData(categories);

            List<Brand> brands = new List<Brand>() { new Brand() { Id = 1, Name = "Trousers", CategoryId = 1 },
                                                     new Brand() { Id = 2, Name = "T-Shirts", CategoryId = 1 },
                                                     new Brand() { Id = 3, Name = "Hoodies", CategoryId = 1 },
                                                     new Brand() { Id = 4, Name = "Shorts", CategoryId = 1 },
                                                     new Brand() { Id = 5, Name = "Dresses", CategoryId = 2 },
                                                     new Brand() { Id = 6, Name = "Skirts", CategoryId = 2 },
                                                     new Brand() { Id = 7, Name = "Jackets", CategoryId = 2 },
                                                     new Brand() { Id = 8, Name = "Blazers", CategoryId = 2 },
                                                     new Brand() { Id = 9, Name = "HoliDays Mood", CategoryId = 3 },
                                                     new Brand() { Id = 10, Name = "Swimwear", CategoryId = 3 },
                                                     new Brand() { Id = 11, Name = "Dresses", CategoryId = 3 },
                                                     new Brand() { Id = 12, Name = "Tops", CategoryId = 3 },
                                                     };

            modelBuilder.Entity<Brand>()
            .HasData(brands);

            List<Product> initialProducts = new List<Product>()
            {
                new Product { Id = 1,Name = "product1", Price = 1000, Discount = 10,Description = "test p1",Model = "M1", BrandId = 1 },
                new Product { Id = 2,Name = "product2", Price = 2000, Discount = 20,Description = "test p2",Model = "M2", BrandId = 1 },
                new Product { Id = 3,Name = "product3", Price = 3000, Discount = 30,Description = "test p3",Model = "M3", BrandId = 2 },
                new Product { Id = 4,Name = "product4", Price = 4000, Discount = 40,Description = "test p4",Model = "M4", BrandId = 3 },
                new Product { Id = 5,Name = "product5", Price = 5000, Discount = 50,Description = "test p5",Model = "M5", BrandId = 3 },
            };

            modelBuilder.Entity<Product>()
            .HasData(initialProducts);
        }
    }
}
