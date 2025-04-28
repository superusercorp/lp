using System.Collections.Generic;
using BusinessEntities;
using System;

namespace Core.Services.Orders
{
    public interface IUpdateOrderService
    {
        void Update(Order order, string customerName, DateTime orderDate, string status, IEnumerable<OrderItem> items);
    }
}
