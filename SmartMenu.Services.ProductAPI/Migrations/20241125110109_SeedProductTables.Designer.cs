﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartMenu.Services.ProductAPI.Data;

#nullable disable

namespace SmartMenu.Services.ProductAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241125110109_SeedProductTables")]
    partial class SeedProductTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.3.23174.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SmartMenu.Services.ProductAPI.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Name = "Kitchen Appliances"
                        },
                        new
                        {
                            CategoryId = 2,
                            Name = "Cleaning Appliances"
                        },
                        new
                        {
                            CategoryId = 3,
                            Name = "Personal Care Appliances"
                        });
                });

            modelBuilder.Entity("SmartMenu.Services.ProductAPI.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageLocalPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1,
                            Description = "Compact and powerful microwave oven with multiple cooking modes and a digital timer.",
                            ImageUrl = "https://placehold.co/600x400",
                            Name = "Microwave Oven",
                            Price = 120.0
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 1,
                            Description = "Energy-efficient refrigerator with spacious compartments and temperature control.",
                            ImageUrl = "https://placehold.co/600x400",
                            Name = "Refrigerator",
                            Price = 450.0
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 2,
                            Description = "High-suction vacuum cleaner with HEPA filters for deep cleaning.",
                            ImageUrl = "https://placehold.co/600x400",
                            Name = "Vacuum Cleaner",
                            Price = 180.0
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 2,
                            Description = "Front-load washing machine with multiple wash settings and energy-saving mode.",
                            ImageUrl = "https://placehold.co/600x400",
                            Name = "Washing Machine",
                            Price = 300.0
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryId = 3,
                            Description = "Ergonomic electric shaver with a rechargeable battery for a smooth shave.",
                            ImageUrl = "https://placehold.co/600x400",
                            Name = "Electric Shaver",
                            Price = 50.0
                        },
                        new
                        {
                            ProductId = 6,
                            CategoryId = 3,
                            Description = "Compact hair dryer with multiple heat and speed settings.",
                            ImageUrl = "https://placehold.co/600x400",
                            Name = "Hair Dryer",
                            Price = 40.0
                        });
                });

            modelBuilder.Entity("SmartMenu.Services.ProductAPI.Models.ProductStore", b =>
                {
                    b.Property<int>("ProductStoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductStoreId"));

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.HasKey("ProductStoreId");

                    b.HasIndex("ProductId");

                    b.HasIndex("StoreId");

                    b.ToTable("ProductStores");

                    b.HasData(
                        new
                        {
                            ProductStoreId = 1,
                            IsAvailable = true,
                            ProductId = 1,
                            StoreId = 1
                        },
                        new
                        {
                            ProductStoreId = 2,
                            IsAvailable = true,
                            ProductId = 2,
                            StoreId = 1
                        },
                        new
                        {
                            ProductStoreId = 3,
                            IsAvailable = true,
                            ProductId = 3,
                            StoreId = 2
                        },
                        new
                        {
                            ProductStoreId = 4,
                            IsAvailable = false,
                            ProductId = 4,
                            StoreId = 2
                        },
                        new
                        {
                            ProductStoreId = 5,
                            IsAvailable = true,
                            ProductId = 5,
                            StoreId = 1
                        },
                        new
                        {
                            ProductStoreId = 6,
                            IsAvailable = true,
                            ProductId = 6,
                            StoreId = 2
                        });
                });

            modelBuilder.Entity("SmartMenu.Services.ProductAPI.Models.Store", b =>
                {
                    b.Property<int>("StoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StoreId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hours")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StoreId");

                    b.ToTable("Stores");

                    b.HasData(
                        new
                        {
                            StoreId = 1,
                            Address = "123 Main Street",
                            Hours = "9am - 9pm",
                            Name = "Downtown Appliance Store",
                            PhoneNumber = "555-1234"
                        },
                        new
                        {
                            StoreId = 2,
                            Address = "456 Elm Street",
                            Hours = "10am - 8pm",
                            Name = "Suburban Appliance Center",
                            PhoneNumber = "555-5678"
                        });
                });

            modelBuilder.Entity("SmartMenu.Services.ProductAPI.Models.Product", b =>
                {
                    b.HasOne("SmartMenu.Services.ProductAPI.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("SmartMenu.Services.ProductAPI.Models.ProductStore", b =>
                {
                    b.HasOne("SmartMenu.Services.ProductAPI.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartMenu.Services.ProductAPI.Models.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Store");
                });
#pragma warning restore 612, 618
        }
    }
}