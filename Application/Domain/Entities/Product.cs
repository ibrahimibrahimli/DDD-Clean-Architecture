using Domain.Common;
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
    }
}
