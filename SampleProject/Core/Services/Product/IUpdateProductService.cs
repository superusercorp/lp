using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Products
{
    public interface IUpdateProductService
    {
        void Update(Product product, string name, string description, string category, decimal price, int stockQuantity, IEnumerable<string> tags);
    }
}
