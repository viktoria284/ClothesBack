namespace ClothesBack.Models
{
    public class ProductWithImage
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public byte[] ImageData { get; set; }
    }
}
