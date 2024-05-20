namespace ClothesBack.Dtos
{
    public class ProductVariantDto
    {
        public Guid ProductVariantId { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int StockQuantity { get; set; }
    }
}
