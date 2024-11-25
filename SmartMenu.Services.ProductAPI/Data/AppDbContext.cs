using SmartMenu.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartMenu.Services.ProductAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<ProductStore> ProductStores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Kitchen Appliances" },
                new Category { CategoryId = 2, Name = "Cleaning Appliances" },
                new Category { CategoryId = 3, Name = "Personal Care Appliances" }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "Microwave Oven",
                    Price = 120.00,
                    Description = "Compact and powerful microwave oven with multiple cooking modes and a digital timer.",
                    ImageUrl = "https://placehold.co/600x400",
                    CategoryId = 1 // Kitchen Appliances
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Refrigerator",
                    Price = 450.00,
                    Description = "Energy-efficient refrigerator with spacious compartments and temperature control.",
                    ImageUrl = "https://placehold.co/600x400",
                    CategoryId = 1 // Kitchen Appliances
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Vacuum Cleaner",
                    Price = 180.00,
                    Description = "High-suction vacuum cleaner with HEPA filters for deep cleaning.",
                    ImageUrl = "https://placehold.co/600x400",
                    CategoryId = 2 // Cleaning Appliances
                },
                new Product
                {
                    ProductId = 4,
                    Name = "Washing Machine",
                    Price = 300.00,
                    Description = "Front-load washing machine with multiple wash settings and energy-saving mode.",
                    ImageUrl = "https://placehold.co/600x400",
                    CategoryId = 2 // Cleaning Appliances
                },
                new Product
                {
                    ProductId = 5,
                    Name = "Electric Shaver",
                    Price = 50.00,
                    Description = "Ergonomic electric shaver with a rechargeable battery for a smooth shave.",
                    ImageUrl = "https://placehold.co/600x400",
                    CategoryId = 3 // Personal Care Appliances
                },
                new Product
                {
                    ProductId = 6,
                    Name = "Hair Dryer",
                    Price = 40.00,
                    Description = "Compact hair dryer with multiple heat and speed settings.",
                    ImageUrl = "https://placehold.co/600x400",
                    CategoryId = 3 // Personal Care Appliances
                }
            );

            // Seed Stores
            modelBuilder.Entity<Store>().HasData(
                new Store
                {
                    StoreId = 1,
                    Name = "Downtown Appliance Store",
                    Address = "123 Main Street",
                    PhoneNumber = "555-1234",
                    Hours = "9am - 9pm"
                },
                new Store
                {
                    StoreId = 2,
                    Name = "Suburban Appliance Center",
                    Address = "456 Elm Street",
                    PhoneNumber = "555-5678",
                    Hours = "10am - 8pm"
                }
            );

            // Seed ProductStore (Store Inventory)
            modelBuilder.Entity<ProductStore>().HasData(
                new ProductStore
                {
                    ProductStoreId = 1,
                    ProductId = 1, // Microwave Oven
                    StoreId = 1,   // Downtown Appliance Store
                    IsAvailable = true
                },
                new ProductStore
                {
                    ProductStoreId = 2,
                    ProductId = 2, // Refrigerator
                    StoreId = 1,   // Downtown Appliance Store
                    IsAvailable = true
                },
                new ProductStore
                {
                    ProductStoreId = 3,
                    ProductId = 3, // Vacuum Cleaner
                    StoreId = 2,   // Suburban Appliance Center
                    IsAvailable = true
                },
                new ProductStore
                {
                    ProductStoreId = 4,
                    ProductId = 4, // Washing Machine
                    StoreId = 2,   // Suburban Appliance Center
                    IsAvailable = false
                },
                new ProductStore
                {
                    ProductStoreId = 5,
                    ProductId = 5, // Electric Shaver
                    StoreId = 1,   // Downtown Appliance Store
                    IsAvailable = true
                },
                new ProductStore
                {
                    ProductStoreId = 6,
                    ProductId = 6, // Hair Dryer
                    StoreId = 2,   // Suburban Appliance Center
                    IsAvailable = true
                }
            );
        }

    }
}
