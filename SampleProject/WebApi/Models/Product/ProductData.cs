using BusinessEntities;

namespace WebApi.Models.Products
{
    public class ProductData : IdObjectData
    {
        public ProductData(Product product) : base(product)
        {
            Name = product.Name;
            Description = product.Description;
            Category = product.Category;
            Price = product.Price;
            StockQuantity = product.StockQuantity;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
