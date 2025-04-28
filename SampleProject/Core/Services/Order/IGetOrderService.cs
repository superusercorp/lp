using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface IGetOrderService
    {
        Order GetOrder(Guid id);

        IEnumerable<Order> GetOrders(DateTime? fromDate = null, DateTime? toDate = null, string customerName = null, string status = null);
    }
}
