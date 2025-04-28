using System;
using System.Collections.Generic;
using Common.Extensions;

namespace BusinessEntities
{
    public class Product : IdObject
    {
        private readonly List<string> _tags = new List<string>();
        private string _name;
        private string _description;
        private decimal _price;
        private int _stockQuantity;
        private string _category;

        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public string Description
        {
            get => _description;
            private set => _description = value;
        }

        public decimal Price
        {
            get => _price;
            private set => _price = value;
        }

        public int StockQuantity
        {
            get => _stockQuantity;
            private set => _stockQuantity = value;
        }

        public string Category
        {
            get => _category;
            private set => _category = value;
        }

        public IEnumerable<string> Tags
        {
            get => _tags;
            private set => _tags.Initialize(value);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Product name must be provided.");
            }
            _name = name;
        }

        public void SetDescription(string description)
        {
            _description = description ?? string.Empty;
        }

        public void SetPrice(decimal price)
        {
            if (price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
            }
            _price = price;
        }

        public void SetStockQuantity(int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Stock quantity cannot be negative.");
            }
            _stockQuantity = quantity;
        }

        public void SetCategory(string category)
        {
            _category = category ?? string.Empty;
        }

        public void SetTags(IEnumerable<string> tags)
        {
            _tags.Initialize(tags);
        }
    }
}
