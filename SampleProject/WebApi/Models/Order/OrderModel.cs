using System;
using System.Collections.Generic;
using BusinessEntities;

namespace WebApi.Models.Orders
{
    public class OrderModel
    {
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
    }
}
