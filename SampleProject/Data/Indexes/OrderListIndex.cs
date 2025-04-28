using System.Linq;
using BusinessEntities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Data.Indexes
{
    public class OrdersListIndex : AbstractIndexCreationTask<Order>
    {
        public OrdersListIndex()
        {
            Map = orders => from order in orders
                            select new
                            {
                                order.CustomerName,
                                order.OrderDate,
                                order.Status
                            };

            Index(x => x.Status, FieldIndexing.NotAnalyzed);
        }
    }
}
