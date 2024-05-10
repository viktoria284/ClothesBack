namespace ClothesBack.Models
{
    public class Image
    {
        public Image(int imageId, int productId, byte[] data)
        {
            ImageId = imageId;
            ProductId = productId;
            Data = data;
        }

        public int ImageId {  get; set; }
        public int ProductId { get; set; } // Внешний ключ
        public byte[] Data { get; set; }

        public Product Product { get; set; } // Навигационное свойство
    }
}
