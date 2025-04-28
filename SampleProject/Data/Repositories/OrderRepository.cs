using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Indexes;
using Raven.Client;

namespace Data.Repositories
{
    [AutoRegister]
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly IDocumentSession _documentSession;

        public OrderRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public IEnumerable<Order> Get(DateTime? fromDate = null, DateTime? toDate = null, string customerName = null, string status = null)
        {
            var query = _documentSession.Advanced.DocumentQuery<Order, OrdersListIndex>();

            var hasFirstParameter = false;

            if (fromDate.HasValue)
            {
                query = query.WhereGreaterThanOrEqual("OrderDate", fromDate.Value);
                hasFirstParameter = true;
            }

            if (toDate.HasValue)
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                else
                {
                    hasFirstParameter = true;
                }
                query = query.WhereLessThanOrEqual("OrderDate", toDate.Value);
            }

            if (!string.IsNullOrEmpty(customerName))
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                else
                {
                    hasFirstParameter = true;
                }
                query = query.Where($"CustomerName:*{customerName}*");
            }

            if (!string.IsNullOrEmpty(status))
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                query = query.WhereEquals("Status", status);
            }

            return query.ToList();
        }

        public void DeleteAll()
        {
            base.DeleteAll<OrdersListIndex>();
        }
    }
}
