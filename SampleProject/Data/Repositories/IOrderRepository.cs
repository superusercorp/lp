using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> Get(DateTime? fromDate = null, DateTime? toDate = null, string customerName = null, string status = null);
        void DeleteAll();
    }
}
