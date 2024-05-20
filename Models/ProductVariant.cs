using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClothesBack.Models
{
    public class ProductVariant
    {
        [Key]
        public Guid ProductVariantId { get; set; }

        [Required]
        public int ProductIdd { get; set; }

        [ForeignKey("ProductIdd")]
        public Product Product { get; set; }

        [Required]
        [StringLength(50)]
        public string Color { get; set; }

        [Required]
        [StringLength(10)]
        public string Size { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    }
}
