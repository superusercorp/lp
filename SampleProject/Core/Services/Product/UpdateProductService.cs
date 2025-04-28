using System.Collections.Generic;
using BusinessEntities;
using Common;

namespace Core.Services.Products
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateProductService : IUpdateProductService
    {
        public void Update(Product product, string name, string description, string category, decimal price, int stockQuantity, IEnumerable<string> tags)
        {
            product.SetName(name);
            product.SetDescription(description);
            product.SetCategory(category);
            product.SetPrice(price);
            product.SetStockQuantity(stockQuantity);
            product.SetTags(tags);
        }
    }
}
