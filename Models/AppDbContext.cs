using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;

namespace ClothesBack.Models
{
    public class AppDbContext : DbContext
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
            modelBuilder.Entity<User>().HasData(
                new User(1, "kostya748", "kostya123"),
                new User(2, "veraVolskaya", "vera123")
            );

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Image) // Один продукт имеет одну картинку
                .WithOne(i => i.Product) // Одна картинка привязана к одному продукту
                .HasForeignKey<Image>(i => i.ProductId); // Внешний ключ в таблице Images
            

            modelBuilder.Entity<Product>().HasData(
                new Product(1, "Balance V3 Seamless Shorts", "Shorts", "Dominate your workout with Balance V3 Seamless Shorts. Crafted with improved softness and stretch, they offer a premium buttery handfeel and a comfortable compressive fit.", 3500),
                new Product(2, "Balance V3 Seamless Zip Jacket", "Jackets", "Crafted with enhanced softness and stretch for premium comfort, it offers a new luxurious buttery handfeel. ", 4500)
            );
        }
    }
}
