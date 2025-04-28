using System.Collections.Generic;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> Get(string category = null, string name = null, decimal? minPrice = null, decimal? maxPrice = null);
        void DeleteAll();
    }
}
