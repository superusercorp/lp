using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class GetOrderService : IGetOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order GetOrder(Guid id)
        {
            return _orderRepository.Get(id);
        }

        public IEnumerable<Order> GetOrders(DateTime? fromDate = null, DateTime? toDate = null, string customerName = null, string status = null)
        {
            return _orderRepository.Get(fromDate, toDate, customerName, status);
        }
    }
}
