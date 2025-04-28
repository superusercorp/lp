using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Products
{
    public interface ICreateProductService
    {
        Product Create(Guid id, string name, string description, string category, decimal price, int stockQuantity, IEnumerable<string> tags);
    }
}
