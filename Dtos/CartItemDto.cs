namespace ClothesBack.Dtos
{
    public class CartItemDto
    {
        public Guid CartItemId { get; set; }
        public Guid ProductVariantId { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
