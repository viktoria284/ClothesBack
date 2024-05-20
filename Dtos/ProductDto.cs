using ClothesBack.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothesBack.Dtos
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        //public List<byte[]> Images { get; set; }
        public byte[] Image { get; set; }
        //public List<ProductVariantDto> Variants { get; set; }
    }
}