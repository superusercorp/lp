using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class CreateOrderService : ICreateOrderService
    {
        private readonly IUpdateOrderService _updateOrderService;
        private readonly IIdObjectFactory<Order> _orderFactory;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderService(IIdObjectFactory<Order> orderFactory, IOrderRepository orderRepository, IUpdateOrderService updateOrderService)
        {
            _orderFactory = orderFactory;
            _orderRepository = orderRepository;
            _updateOrderService = updateOrderService;
        }

        public Order Create(Guid id, string customerName, DateTime orderDate, string status, IEnumerable<OrderItem> items)
        {
            var order = _orderFactory.Create(id);
            _updateOrderService.Update(order, customerName, orderDate, status, items);
            _orderRepository.Save(order);
            return order;
        }
    }
}
