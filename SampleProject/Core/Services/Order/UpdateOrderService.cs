using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;

namespace Core.Services.Orders
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateOrderService : IUpdateOrderService
    {
        public void Update(Order order, string customerName, DateTime orderDate, string status, IEnumerable<OrderItem> items)
        {
            order.SetCustomerName(customerName);
            order.SetOrderDate(orderDate);
            order.SetStatus(status);
            order.SetItems(items);
        }
    }
}
