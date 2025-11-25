namespace Domain.ValueObjects
{
    public sealed class ProductName : IEquatable<ProductName>
    {
        public string Value { get; }

        private ProductName(string value)
        {
            Value = value;
        }

        public static ProductName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Product name cannot be empty", nameof(value));

            if (value.Length < 3)
                throw new ArgumentException("Product name must be at least 3 characters", nameof(value));

            if (value.Length > 200)
                throw new ArgumentException("Product name cannot exceed 200 characters", nameof(value));

            return new ProductName(value.Trim());
        }

        public bool Equals(ProductName? other)
        {
            if (other is null) return false;
            return Value == other.Value;
        }

        public override bool Equals(object? obj) => obj is ProductName name && Equals(name);

        public override int GetHashCode() => Value.GetHashCode();

        public static implicit operator string(ProductName productName) => productName.Value;
    }
}
