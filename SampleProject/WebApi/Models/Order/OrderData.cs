using System;
using System.Collections.Generic;
using BusinessEntities;

namespace WebApi.Models.Orders
{
    public class OrderData : IdObjectData
    {
        public OrderData(Order order) : base(order)
        {
            CustomerName = order.CustomerName;
            OrderDate = order.OrderDate;
            Status = order.Status;
            Items = order.Items;
            TotalAmount = order.TotalAmount;
        }

        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
