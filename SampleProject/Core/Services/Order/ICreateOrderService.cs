using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface ICreateOrderService
    {
        Order Create(Guid id, string customerName, DateTime orderDate, string status, IEnumerable<OrderItem> items);
    }
}
