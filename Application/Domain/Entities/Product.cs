using Domain.Common;
using Domain.Events;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public sealed class Product : BaseEntity
    {
        public ProductName Name { get; private set; }
        public string Description { get; private set; }
        public Money Price { get; private set; }
        public int StockQuantity { get; private set; }
        public string? Sku { get; private set; }
        public bool IsActive { get; private set; }

        private Product() { }

        private Product(ProductName name, string description, Money price, int stockQuantity, string? sku)
        {
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
            Sku = sku;
            IsActive = true;
        }

        public static Product Create(string name, string description, decimal price,
        int stockQuantity, string? sku = null, string currency = "USD")
        {
            var productName = ProductName.Create(name);
            var productPrice = Money.Create(price, currency);

            if (stockQuantity < 0)
                throw new ArgumentException("Stock quantity cannot be negative", nameof(stockQuantity));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty", nameof(description));

            var product = new Product(productName, description, productPrice, stockQuantity, sku);

            // Domain Event raise edirik
            product.AddDomainEvent(new ProductCreatedEvent(product.Id, product.Name, product.Price));

            return product;
        }
    }
}
