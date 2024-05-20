using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;

namespace ClothesBack.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public AppDbContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductVariants)
                .WithOne(pv => pv.Product)
                .HasForeignKey(pv => pv.ProductId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Images)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId);

            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.ProductVariant)
                .WithMany()
                .HasForeignKey(ci => ci.ProductVariantId);

            modelBuilder.Entity<Product>().HasData(
                new Product(Guid.NewGuid(), "Phys Ed Graphic T-Shirt", "T-Shirts", "Premium heavyweight fabric for comfort that hits different. Physical Education graphic to chest", "Black", 30.00m),
                new Product(Guid.NewGuid(), "Phys Ed Hoodie", "Hoodies", "From rest day relaxing to brunch with the girls, elevate your off-duty vibe in the Phys Ed collection.", "Black", 82.00m),
                new Product(Guid.NewGuid(), "Training Oversized Fleece Sweatshirt", "Sweatshirts", "Soft, brushed back fabric inside for warmth and comfort. Ribbed hem and cuffs for a clean fit. Oversized fit", "Black", 50.00m),
                new Product(Guid.NewGuid(), "Balance V3 Seamless Zip Jacket", "Jackets", "Crafted with enhanced softness and stretch for premium comfort, it offers a new luxurious buttery handfeel.", "Black", 45.00m),
                new Product(Guid.NewGuid(), "Balance V3 Seamless Crop Top", "Tops", "Re-designed with enhanced softness for a luxurious handfeel and new improved stretch for better comfort, it ensures optimal performance.", "Black", 30.00m),
                new Product(Guid.NewGuid(), "Balance V3 Seamless Shorts", "Shorts", "Dominate your workout with Balance V3 Seamless Shorts. Crafted with improved softness and stretch, they offer a premium buttery handfeel and a comfortable compressive fit.", "Black", 35.00m),
                new Product(Guid.NewGuid(), "Balance V3 Seamless Leggings", "Leggings", "With an increased fabric weight, these leggings ensure a squat-proof finish, empowering you during your workouts. The refined waistband depth provides a true mid-high waist fit, offering essential support.", "Black", 48.00m),
                new Product(Guid.NewGuid(), "GFX Crew Socks 7PK", "Socks", "Lifters don't live by Mondays and Tuesdays. We live by Chest Days and Leg Days. Back Days and Rest Days. And these socks know what's up", "Black", 40.00m)
                );

            /*List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);*/
        }
    }
}
