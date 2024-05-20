namespace ClothesBack.Dtos
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<byte[]> Images { get; set; }
        public List<ProductVariantDto> Variants { get; set; }
    }
}