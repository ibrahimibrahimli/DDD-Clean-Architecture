using Domain.Common;
using Domain.Events.Product;
using Domain.Events.Products;
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

        public void UpdateDetails(string name, string description, decimal price, string currency = "USD")
        {
            var oldName = Name.Value;
            var oldPrice = Price;

            Name = ProductName.Create(name);
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Price = Money.Create(price, currency);

            MarkAsUpdated();

            // Domain Event
            AddDomainEvent(new ProductUpdatedEvent(Id, Name, oldName, Price, oldPrice));
        }

        public void UpdateStock(int quantity)
        {
            if (quantity < 0)
                throw new ArgumentException("Stock quantity cannot be negative", nameof(quantity));

            StockQuantity = quantity;
            MarkAsUpdated();
        }

        public void IncreaseStock(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive", nameof(amount));

            StockQuantity += amount;
            MarkAsUpdated();
        }

        public void DecreaseStock(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive", nameof(amount));

            if (StockQuantity < amount)
                throw new InvalidOperationException("Insufficient stock");

            StockQuantity -= amount;
            MarkAsUpdated();

            if (StockQuantity == 0)
                AddDomainEvent(new ProductOutOfStockEvent(Id, Name));
        }

        public void Activate()
        {
            if (IsActive) return;

            IsActive = true;
            MarkAsUpdated();
        }

        public void Deactivate()
        {
            if (!IsActive) return;

            IsActive = false;
            MarkAsUpdated();
        }

        public void Delete()
        {
            MarkAsDeleted();
            AddDomainEvent(new ProductDeletedEvent(Id, Name));
        }
    }
}
