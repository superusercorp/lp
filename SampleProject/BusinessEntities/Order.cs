using System;
using System.Collections.Generic;
using Common.Extensions;

namespace BusinessEntities
{
    public class Order : IdObject
    {
        private readonly List<OrderItem> _items = new List<OrderItem>();
        private string _customerName;
        private DateTime _orderDate;
        private decimal _totalAmount;
        private string _status;

        public string CustomerName
        {
            get => _customerName;
            private set => _customerName = value;
        }

        public DateTime OrderDate
        {
            get => _orderDate;
            private set => _orderDate = value;
        }

        public decimal TotalAmount
        {
            get => _totalAmount;
            private set => _totalAmount = value;
        }

        public string Status
        {
            get => _status;
            private set => _status = value;
        }

        public IEnumerable<OrderItem> Items
        {
            get => _items;
            private set => _items.Initialize(value);
        }

        public void SetCustomerName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Customer name must be provided.");
            }
            _customerName = name;
        }

        public void SetOrderDate(DateTime orderDate)
        {
            _orderDate = orderDate;
        }

        public void SetStatus(string status)
        {
            _status = status ?? "Pending";
        }

        public void SetItems(IEnumerable<OrderItem> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items), "Order items must be provided.");
            }
            _items.Initialize(items);
            UpdateTotalAmount();
        }

        private void UpdateTotalAmount()
        {
            decimal total = 0;
            foreach (var item in _items)
            {
                total += item.TotalPrice;
            }
            _totalAmount = total;
        }
    }
}
