namespace ClothesBack.Dtos
{
    public class CartItemDto
    {
        public string UserId {  get; set; }
        public Guid CartItemId { get; set; }
        public Guid ProductVariantId { get; set; }
        public string ProductName { get; set; }
        public byte[] Image { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
