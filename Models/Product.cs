using System.ComponentModel.DataAnnotations;

namespace ClothesBack.Models
{
    public class Product
    {
        /*public Product(Guid productId, string productName, string category, string description, string color, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            Category = category;
            Description = description;
            Color = color;
            Price = price;
            ProductVariants = new List<ProductVariant>();
            Images = new List<Image>();
        }*/

        [Key]
        public Guid ProductId { get; set; }

        [Required]
        [StringLength(255)]
        public string ProductName { get; set; }

        [Required]
        [StringLength(255)]
        public string Category { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string Color { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        public byte[] Image { get; set; }
        public ICollection<ProductVariant> ProductVariants { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
