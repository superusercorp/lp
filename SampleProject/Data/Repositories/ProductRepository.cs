using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Indexes;
using Raven.Client;

namespace Data.Repositories
{
    [AutoRegister]
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly IDocumentSession _documentSession;

        public ProductRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public IEnumerable<Product> Get(string category = null, string name = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            var query = _documentSession.Advanced.DocumentQuery<Product, ProductsListIndex>();

            var hasFirstParameter = false;

            if (!string.IsNullOrEmpty(category))
            {
                query = query.WhereEquals("Category", category);
                hasFirstParameter = true;
            }

            if (!string.IsNullOrEmpty(name))
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                else
                {
                    hasFirstParameter = true;
                }
                query = query.Where($"Name:*{name}*");
            }

            if (minPrice.HasValue)
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                else
                {
                    hasFirstParameter = true;
                }
                query = query.WhereGreaterThanOrEqual("Price", minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                query = query.WhereLessThanOrEqual("Price", maxPrice.Value);
            }

            return query.ToList();
        }

        public void DeleteAll()
        {
            base.DeleteAll<ProductsListIndex>();
        }
    }
}
