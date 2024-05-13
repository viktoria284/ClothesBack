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
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public AppDbContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Images) // Один продукт имеет одну картинку
                .WithOne(i => i.Product) // Одна картинка привязана к одному продукту
                .HasForeignKey(i => i.ProductId); // Внешний ключ в таблице Images
            

            modelBuilder.Entity<Product>().HasData(
                new Product(1, "Balance V3 Seamless Shorts", "Shorts", "Dominate your workout with Balance V3 Seamless Shorts. Crafted with improved softness and stretch, they offer a premium buttery handfeel and a comfortable compressive fit.", 3500),
                new Product(2, "Balance V3 Seamless Zip Jacket", "Jackets", "Crafted with enhanced softness and stretch for premium comfort, it offers a new luxurious buttery handfeel. ", 4500),
                new Product(3, "Balance V3 Seamless Crop Top", "Tops", "Re-designed with enhanced softness for a luxurious handfeel and new improved stretch for better comfort, it ensures optimal performance. ", 3000),
                new Product(4, "Balance V3 Seamless Leggings", "Leggings", "With an increased fabric weight, these leggings ensure a squat-proof finish, empowering you during your workouts. The refined waistband depth provides a true mid-high waist fit, offering essential support.", 4800),
                new Product(5, "Phys Ed Graphic T-Shirt", "T-Shirts", "Premium heavyweight fabric for comfort that hits different. Physical Education graphic to chest", 3000),
                new Product(6, "Phys Ed Hoodie", "Hoodies", "From rest day relaxing to brunch with the girls, elevate your off-duty vibe in the Phys Ed collection.", 8200)
            );

            List<IdentityRole> roles = new List<IdentityRole>
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
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
