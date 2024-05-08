namespace ClothesBack.Models
{
    public class Product
    {
        public Product(int productId, string productName, string category, string description, int price)
        {
            ProductId = productId;
            ProductName = productName;
            Category = category;
            Description = description;
            Price = price;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Price {  get; set; }
    }
}
