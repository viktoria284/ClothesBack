namespace ClothesBack.Models
{
    public class ProductWithImage
    {
        public Guid ProductVariantId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[] ImageData { get; set; }
    }
}
