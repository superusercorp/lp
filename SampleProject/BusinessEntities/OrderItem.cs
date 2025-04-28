using System;

namespace BusinessEntities
{
    public class OrderItem : IdObject
    {
        private string _productName;
        private decimal _unitPrice;
        private int _quantity;

        public string ProductName
        {
            get => _productName;
            private set => _productName = value;
        }

        public decimal UnitPrice
        {
            get => _unitPrice;
            private set => _unitPrice = value;
        }

        public int Quantity
        {
            get => _quantity;
            private set => _quantity = value;
        }

        public decimal TotalPrice => _unitPrice * _quantity;

        public void SetProductName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Product name must be provided.");
            }
            _productName = name;
        }

        public void SetUnitPrice(decimal price)
        {
            if (price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price), "Unit price cannot be negative.");
            }
            _unitPrice = price;
        }

        public void SetQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
            }
            _quantity = quantity;
        }
    }
}
