using System.Linq;
using BusinessEntities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Data.Indexes
{
    public class ProductsListIndex : AbstractIndexCreationTask<Product>
    {
        public ProductsListIndex()
        {
            Map = products => from product in products
                              select new
                              {
                                  product.Name,
                                  product.Category,
                                  product.Price
                              };

            Index(x => x.Category, FieldIndexing.NotAnalyzed);
        }
    }
}
