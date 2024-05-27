namespace ClothesBack.Dtos
{
    public class AddToCartDto
    {
        public string UserId { get; set; }
        public Guid ProductVariantId { get; set; }
        public int Quantity { get; set; }
    }
}
