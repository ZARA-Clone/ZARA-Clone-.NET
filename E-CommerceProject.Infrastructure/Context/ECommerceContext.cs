using E_CommerceProject.Models;
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

            List<Brand> initialBrands = new List<Brand>()
            {
                new Brand() { Id = 1, Name = "Lenovo"},
                new Brand() { Id = 2, Name = "Dell"},
                new Brand() { Id = 3, Name = "HP"},
                new Brand() { Id = 4, Name = "Apple"},
                new Brand() { Id = 5, Name = "Asus"},
                new Brand() { Id = 6, Name = "Acer"},
                new Brand() { Id = 7, Name = "Microsoft"},
                new Brand() { Id = 8, Name = "MSI"},
                new Brand() { Id = 9, Name = "Alienware"},
                new Brand() { Id = 10, Name = "Samsung"},
                new Brand() { Id = 11, Name = "Huawei"},
                new Brand() { Id = 12, Name = "Fujitsu"},
                new Brand() { Id = 13, Name = "Google"},
                new Brand() { Id = 14, Name = "Razer"},
            };

            modelBuilder.Entity<Brand>()
                        .HasData(initialBrands);
            
            List<Product> initialProducts = new List<Product>()
            {
                new Product
                {
                    Id = 1,
                    Name = "Apple MacBook Air",
                    Price = 40000,
                    Discount = 50,
                    Description = "The Apple MacBook Air is a lightweight and portable laptop with excellent battery life.",
                    Model = "MacBook Air",
                    BrandId = 4
                },
                new Product
                {
                    Id = 2,
                    Name = "Apple MacBook Pro",
                    Price = 80000,
                    Discount =20,
                    Description = "The Apple MacBook Pro is a high-performance laptop loved by professionals.",
                    Model = "MacBook Pro",
                    BrandId = 4
                },
                new Product
                {
                    Id = 3,
                    Name = "Dell XPS 13",
                    Price = 40000,
                    Discount = 0,
                    Description = "The Dell XPS 13 is a sleek and powerful laptop with a stunning display.",
                    Model = "XPS 13",
                    BrandId = 2
                },
                new Product
                {
                    Id = 4,
                    Name = "Dell Inspiron 15",
                    Price = 35000,
                    Discount = 13,
                    Description = "The Dell Inspiron 15 is a versatile laptop suitable for everyday use.",
                    Model = "Inspiron 15",
                    BrandId = 2
                },
                new Product
                {
                    Id = 5,
                    Name = "HP Spectre x360",
                    Price = 25000,
                    Discount = 15,
                    Description = "The HP Spectre x360 is a stylish 2-in-1 laptop with powerful performance.",
                    Model = "Spectre x360",
                    BrandId = 3
                },
                new Product
                {
                    Id = 6,
                    Name = "HP Pavilion 14",
                    Price = 15000,
                    Discount = 60,
                    Description = "The HP Pavilion 14 is a budget-friendly laptop with decent specifications.",
                    Model = "Pavilion 14",
                    BrandId = 3
                },
                new Product
                {
                    Id = 7,
                    Name = "Apple MacBook Air",
                    Price = 28000,
                    Discount = 10,
                    Description = "The Apple MacBook Air is a lightweight and portable laptop with excellent battery life.",
                    Model = "MacBook Air",
                    BrandId = 4
                },
                new Product
                {
                    Id = 8,
                    Name = "Apple MacBook Pro",
                    Price = 30000,
                    Discount = 12,
                    Description = "The Apple MacBook Pro is a high-performance laptop loved by professionals.",
                    Model = "MacBook Pro",
                    BrandId = 4
                },
                new Product
                {
                    Id = 9,
                    Name = "Apple iMac",
                    Price = 16000,
                    Discount = 0,
                    Description = "The Apple iMac is a sleek and powerful all-in-one desktop computer.",
                    Model = "iMac",
                    BrandId = 4
                },
                new Product
                {
                    Id = 10,
                    Name = "Dell XPS 13",
                    Price = 14000,
                    Discount = 90,
                    Description = "The Dell XPS 13 is a sleek and powerful laptop with a stunning display.",
                    Model = "XPS 13",
                    BrandId = 2
                },
                new Product
                {
                    Id = 11,
                    Name = "Dell Inspiron 15",
                    Price = 30000,
                    Discount = 18,
                    Description = "The Dell Inspiron 15 is a versatile laptop suitable for everyday use.",
                    Model = "Inspiron 15",
                    BrandId = 2
                },
                new Product
                {
                    Id = 12,
                    Name = "Dell G5 Gaming Desktop",
                    Price = 38000,
                    Discount = 20,
                    Description = "The Dell G5 Gaming Desktop is a powerful gaming machine with immersive graphics.",
                    Model = "G5 Gaming Desktop",
                    BrandId = 2
                },
                new Product
                {
                    Id = 13,
                    Name = "HP Spectre x360",
                    Price = 26000,
                    Discount = 19,
                    Description = "The HP Spectre x360 is a stylish 2-in-1 laptop with powerful performance.",
                    Model = "Spectre x360",
                    BrandId = 3
                },
                new Product
                {
                    Id = 14,
                    Name = "HP Pavilion 14",
                    Price = 6000,
                    Discount = 0,
                    Description = "The HP Pavilion 14 is a budget-friendly laptop with decent specifications.",
                    Model = "Pavilion 14",
                    BrandId = 3
                },
                new Product
                {
                    Id = 15,
                    Name = "HP EliteBook 840",
                    Price = 50000,
                    Discount = 80,
                    Description = "The HP EliteBook 840 is a business laptop with top-notch security features.",
                    Model = "EliteBook 840",
                    BrandId = 3
                },
                new Product
                {
                    Id = 16,
                    Name = "Apple MacBook Air",
                    Price = 18000,
                    Discount = 15,
                    Description = "The Apple MacBook Air is a lightweight and portable laptop with excellent battery life.",
                    Model = "MacBook Air",
                    BrandId = 4
                },
                new Product
                {
                    Id = 17,
                    Name = "Dell XPS 13",
                    Price = 13000,
                    Discount = 5,
                    Description = "The Dell XPS 13 is a sleek and powerful laptop with a stunning display.",
                    Model = "XPS 13",
                    BrandId = 2
                },
                new Product
                {
                    Id = 18,
                    Name = "HP Spectre x360",
                    Price = 12000,
                    Discount = 10,
                    Description = "The HP Spectre x360 is a stylish 2-in-1 laptop with powerful performance.",
                    Model = "Spectre x360",
                    BrandId = 3
                },
                new Product
                {
                    Id = 19,
                    Name = "Lenovo ThinkCentre M720",
                    Price = 15000,
                    Discount = 6,
                    Description = "The Lenovo ThinkCentre M720 is a compact and reliable desktop computer for business use.",
                    Model = "ThinkCentre M720",
                    BrandId = 1
                },
                new Product
                {
                    Id = 20,
                    Name = "ASUS ROG Strix G15",
                    Price = 80000,
                    Discount = 60,
                    Description = "The ASUS ROG Strix G15 is a powerful gaming desktop with RGB lighting and high-performance components.",
                    Model = "ROG Strix G15",
                    BrandId = 5
                },
                new Product
                {
                    Id = 21,
                    Name = "Acer Aspire TC",
                    Price = 18000,
                    Discount = 15,
                    Description = "The Acer Aspire TC is a budget-friendly desktop computer with decent performance.",
                    Model = "Aspire TC",
                    BrandId = 6
                },
                new Product
                {
                    Id = 22,
                    Name = "Dell Inspiron 27",
                    Price = 22000,
                    Discount = 10,
                    Description = "The Dell Inspiron 27 is an all-in-one desktop computer with a large display and powerful performance.",
                    Model = "Inspiron 27",
                    BrandId = 2
                },
                new Product
                {
                    Id = 23,
                    Name = "ASUS ZenBook Pro",
                    Price = 28000,
                    Discount = 15,
                    Description = "The ASUS ZenBook Pro is a premium laptop with a stunning 4K display and high-performance components.",
                    Model = "ZenBook Pro",
                    BrandId = 5
                },
                new Product
                {
                    Id = 24,
                    Name = "HP Pavilion Gaming Desktop",
                    Price = 15000,
                    Discount = 80,
                    Description = "The HP Pavilion Gaming Desktop is a gaming powerhouse with advanced graphics and smooth gameplay.",
                    Model = "Pavilion Gaming Desktop",
                    BrandId = 3
                },
                new Product
                {
                    Id = 25,
                    Name = "Lenovo Legion Y540",
                    Price = 20000,
                    Discount = 12,
                    Description = "The Lenovo Legion Y540 is a gaming laptop with powerful hardware and immersive gaming experience.",
                    Model = "Legion Y540",
                    BrandId = 1
                },
                new Product
                {
                    Id = 26,
                    Name = "Apple iMac",
                    Price = 24000,
                    Discount = 20,
                    Description = "The Apple iMac is a sleek all-in-one desktop computer with a stunning Retina display and powerful performance.",
                    Model = "iMac",
                    BrandId = 4
                },
                new Product
                {
                    Id = 27,
                    Name = "Dell G5 Gaming Laptop",
                    Price = 18000,
                    Discount = 10,
                    Description = "The Dell G5 is a gaming laptop with high-performance hardware and immersive gaming features.",
                    Model = "G5 Gaming Laptop",
                    BrandId = 2
                },
                new Product
                {
                    Id = 28,
                    Name = "HP Envy 15",
                    Price = 16000,
                    Discount = 15,
                    Description = "The HP Envy 15 is a premium laptop with a sleek design and powerful performance for multimedia and productivity tasks.",
                    Model = "Envy 15",
                    BrandId = 3
                },
                new Product
                {
                    Id = 29,
                    Name = "Lenovo IdeaCentre 5",
                    Price = 8990,
                    Discount = 50,
                    Description = "The Lenovo IdeaCentre 5 is a compact and versatile desktop computer suitable for home and office use.",
                    Model = "IdeaCentre 5",
                    BrandId = 1
                },
                new Product
                {
                    Id = 30,
                    Name = "ASUS VivoBook S15",
                    Price = 9990,
                    Discount = 0,
                    Description = "The ASUS VivoBook S15 is a stylish and lightweight laptop with a vibrant display and long battery life.",
                    Model = "VivoBook S15",
                    BrandId = 5
                },
                new Product
                {
                    Id = 31,
                    Name = "Samsung Galaxy Book Pro",
                    Price = 14990,
                    Discount = 10,
                    Description = "The Samsung Galaxy Book Pro is a thin and lightweight laptop with a stunning AMOLED display and powerful performance.",
                    Model = "Galaxy Book Pro",
                    BrandId = 10
                },
                new Product
                {
                    Id = 32,
                    Name = "Dell Alienware Aurora R10",
                    Price = 28000,
                    Discount = 20,
                    Description = "The Dell Alienware Aurora R10 is a high-performance gaming desktop with powerful hardware and customizable lighting.",
                    Model = "Alienware Aurora R10",
                    BrandId = 2
                },
                new Product
                {
                    Id = 33,
                    Name = "HP Omen 15",
                    Price = 17999,
                    Discount = 15,
                    Description = "The HP Omen 15 is a gaming laptop with a sleek design, high-refresh-rate display, and powerful performance for gaming enthusiasts.",
                    Model = "Omen 15",
                    BrandId = 3
                },
                new Product
                {
                    Id = 34,
                    Name = "Apple MacBook Air",
                    Price = 12990,
                    Discount = 10,
                    Description = "The Apple MacBook Air is a lightweight and portable laptop with a stunning Retina display and all-day battery life.",
                    Model = "MacBook Air",
                    BrandId = 4
                },
                new Product
                {
                    Id = 35,
                    Name = "Razer Blade 15",
                    Price = 23990,
                    Discount = 15,
                    Description = "The Razer Blade 15 is a premium gaming laptop with a sleek design, high-refresh-rate display, and powerful performance.",
                    Model = "Blade 15",
                    BrandId = 14
                },
                new Product
                {
                    Id = 36,
                    Name = "Lenovo ThinkPad X1 Carbon",
                    Price = 18990,
                    Discount = 20,
                    Description = "The Lenovo ThinkPad X1 Carbon is a premium business laptop with a durable build, long battery life, and top-notch performance.",
                    Model = "ThinkPad X1 Carbon",
                    BrandId = 1
                },
                new Product
                {
                    Id = 37,
                    Name = "ASUS ROG Zephyrus G14",
                    Price = 17000,
                    Discount = 5,
                    Description = "The ASUS ROG Zephyrus G14 is a powerful gaming laptop with an ultra-portable design and impressive performance.",
                    Model = "ROG Zephyrus G14",
                    BrandId = 5
                },
                new Product
                {
                    Id = 38,
                    Name = "MSI GS66 Stealth",
                    Price = 23999,
                    Discount = 13,
                    Description = "The MSI GS66 Stealth is a high-performance gaming laptop with a sleek design and powerful components.",
                    Model = "GS66 Stealth",
                    BrandId = 8
                },
                new Product
                {
                    Id = 39,
                    Name = "MSI Prestige 14",
                    Price = 15990,
                    Discount = 15,
                    Description = "The MSI Prestige 14 is a stylish and powerful laptop designed for creative professionals.",
                    Model = "Prestige 14",
                    BrandId = 8
                },
                new Product
                {
                    Id = 40,
                    Name = "Microsoft Surface Laptop 4",
                    Price = 23000,
                    Discount = 15,
                    Description = "The Microsoft Surface Laptop 4 is a sleek and versatile laptop with a premium design and excellent performance.",
                    Model = "Surface Laptop 4",
                    BrandId = 7
                },
                new Product
                {
                    Id = 41,
                    Name = "Microsoft Surface Pro 7",
                    Price = 20000,
                    Discount = 10,
                    Description = "The Microsoft Surface Pro 7 is a powerful 2-in-1 tablet-laptop hybrid with a detachable keyboard and versatile functionality.",
                    Model = "Surface Pro 7",
                    BrandId = 7
                },
            };
            
            modelBuilder.Entity<Product>()
                        .HasData(initialProducts);

        }
    }
}
