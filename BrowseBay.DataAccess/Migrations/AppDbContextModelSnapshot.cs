﻿// <auto-generated />
using System;
using BrowseBay.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BrowseBay.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BrowseBay.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Clothing & Apparel"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Electronics"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Home & Kitchen"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Health & Beauty"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Sports & Outdoors"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Books & Media"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Toys & Games"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Automotive"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Pets"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Jewelry & Accessories"
                        });
                });

            modelBuilder.Entity("BrowseBay.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ImageSource")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Sleek black tee: Style redefined. Elevate your look effortlessly! 🔥 #FashionEssential",
                            ImageSource = "~/images/OIP.jpg",
                            Name = "T-Shirt",
                            Price = 299.0
                        },
                        new
                        {
                            Id = 2,
                            Description = "Unleash limitless power with our latest cellphone innovation!",
                            ImageSource = "~/images/cellphone.jpg",
                            Name = "Cellphone",
                            Price = 13999.0
                        },
                        new
                        {
                            Id = 3,
                            Description = "Unleash precision in the palm of your hand. Elevate your tools with our sleek knife.",
                            ImageSource = "~/images/ec3596459302e2e8e4d586517816a69a.jpg",
                            Name = "Knife",
                            Price = 240.0
                        },
                        new
                        {
                            Id = 4,
                            Description = "Indulge in luxury with our hydrating lotion. Elevate your skincare routine effortlessly.",
                            ImageSource = "~/images/lotion.jpg",
                            Name = "Lotion",
                            Price = 250.0
                        },
                        new
                        {
                            Id = 5,
                            Description = "Step up your game with our stylish rubber shoes. Elevate your look with every stride.",
                            ImageSource = "~/images/rubbershoes.jpg",
                            Name = "Rubber Shoes",
                            Price = 5500.0
                        },
                        new
                        {
                            Id = 6,
                            Description = "Master clean code principles. Robert Martin's essential guide.",
                            ImageSource = "~/images/cleancode.jpg",
                            Name = "Clean Code",
                            Price = 2890.0
                        },
                        new
                        {
                            Id = 7,
                            Description = "Immerse in endless adventures. Explore, create, survive. Minecraft awaits!",
                            ImageSource = "~/images/Minecraft.jpg",
                            Name = "Minecraft",
                            Price = 150.0
                        },
                        new
                        {
                            Id = 8,
                            Description = "Upgrade your cleaning game with our durable fiber cloth.",
                            ImageSource = "~/images/fibrecloth.jpg",
                            Name = "Fibre Cloth",
                            Price = 40.0
                        },
                        new
                        {
                            Id = 9,
                            Description = "Pure nourishment for your pet. Goat's milk: natural goodness.",
                            ImageSource = "~/images/goatsmilk.jpg",
                            Name = "Goat's Milk",
                            Price = 380.0
                        },
                        new
                        {
                            Id = 10,
                            Description = "Elegant luxury, timeless beauty. Elevate your style with 14k gold.",
                            ImageSource = "~/images/necklace.jpg",
                            Name = "14K Gold Necklace",
                            Price = 21500.0
                        },
                        new
                        {
                            Id = 11,
                            Description = "Powerful laptop with high-speed performance. Perfect for work or entertainment on the go.",
                            ImageSource = "~/images/laptop.jpg",
                            Name = "Laptop",
                            Price = 50000.0
                        },
                        new
                        {
                            Id = 12,
                            Description = "Track your fitness, receive notifications, and more, all from your wrist.",
                            ImageSource = "~/images/smartwatch.jpg",
                            Name = "Smartwatch",
                            Price = 9999.9500000000007
                        },
                        new
                        {
                            Id = 13,
                            Description = "Enjoy crisp sound quality and freedom from wires with these wireless earbuds.",
                            ImageSource = "~/images/wirelessearbuds.jpg",
                            Name = "Wireless Earbuds",
                            Price = 3999.9499999999998
                        },
                        new
                        {
                            Id = 14,
                            Description = "Take your music anywhere with this portable Bluetooth speaker.",
                            ImageSource = "~/images/bluetoothspeaker.jpg",
                            Name = "Portable Bluetooth Speaker",
                            Price = 2499.9499999999998
                        },
                        new
                        {
                            Id = 15,
                            Description = "Monitor your health and track your fitness goals with this sleek fitness tracker.",
                            ImageSource = "~/images/fitnesstracker.jpg",
                            Name = "Fitness Tracker",
                            Price = 2995.0
                        },
                        new
                        {
                            Id = 16,
                            Description = "Brew your favorite coffee just the way you like it.",
                            ImageSource = "~/images/coffeemaker.jpg",
                            Name = "Coffee Maker",
                            Price = 6850.5
                        },
                        new
                        {
                            Id = 17,
                            Description = "Gentle on gums, powerful on plaque.",
                            ImageSource = "~/images/electrictoothbrush.jpg",
                            Name = "Electric Toothbrush",
                            Price = 1999.0
                        },
                        new
                        {
                            Id = 18,
                            Description = "Capture every moment with stunning clarity using this digital camera.",
                            ImageSource = "~/images/digitalcamera.jpg",
                            Name = "Digital Camera",
                            Price = 14560.6
                        },
                        new
                        {
                            Id = 19,
                            Description = "Enjoy healthier cooking without sacrificing flavor with this air fryer.",
                            ImageSource = "~/images/airfryer.jpg",
                            Name = "Air Fryer",
                            Price = 4499.0
                        },
                        new
                        {
                            Id = 20,
                            Description = "Never run out of battery again with this portable power bank.",
                            ImageSource = "~/images/powerbank.jpg",
                            Name = "Portable Power Bank",
                            Price = 1499.0
                        });
                });

            modelBuilder.Entity("BrowseBay.Models.ProductCategory", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProductId", "CategoryId")
                        .IsUnique();

                    b.ToTable("ProductCategories");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 2,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 3,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 4,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryId = 5,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 6,
                            CategoryId = 6,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 7,
                            CategoryId = 7,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 7,
                            CategoryId = 6,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 8,
                            CategoryId = 8,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 9,
                            CategoryId = 9,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 10,
                            CategoryId = 10,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 11,
                            CategoryId = 2,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 12,
                            CategoryId = 2,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 12,
                            CategoryId = 5,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 12,
                            CategoryId = 10,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 13,
                            CategoryId = 2,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 14,
                            CategoryId = 2,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 15,
                            CategoryId = 2,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 15,
                            CategoryId = 5,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 15,
                            CategoryId = 10,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 16,
                            CategoryId = 2,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 17,
                            CategoryId = 2,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 18,
                            CategoryId = 2,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 19,
                            CategoryId = 2,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 19,
                            CategoryId = 3,
                            Id = 0
                        },
                        new
                        {
                            ProductId = 20,
                            CategoryId = 2,
                            Id = 0
                        });
                });

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("ProductsId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("CategoryProduct");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("BrowseBay.Models.ProductCategory", b =>
                {
                    b.HasOne("BrowseBay.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BrowseBay.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.HasOne("BrowseBay.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BrowseBay.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
