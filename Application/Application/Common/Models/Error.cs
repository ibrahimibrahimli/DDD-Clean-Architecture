namespace Application.Common.Models
{
    public sealed record Error(string Code, string Message)
    {
        static readonly Error None = new (string.Empty, string.Empty);
        static readonly Error NullValue = new("Error.NullValue", "Null value provided");


        public static class Product
        {
            public static Error NotFound(Guid id) => new("Product.NotFound", $"Product with ID '{id}' was not found");
            public static Error InvalidName => new("Product.InvalidName", "Product name is invalid");
            public static Error InvalidPrice => new("Product.InvalidPrice", "Product price must be positive");
            public static Error InvalidStock => new("Product.InvalidStock", "Stock quantity cannot be negative");
            public static Error AlreadyDeleted => new("Product.AlreadyDeleted", "Product is already deleted");
            public static Error SkuAlreadyExists(string sku) => new("Product.SkuExists", $"Product with SKU '{sku}' already exists");
            public static Error InsufficientStock => new("Product.InsufficientStock", "Insufficient stock quantity");
        }
    }
}
