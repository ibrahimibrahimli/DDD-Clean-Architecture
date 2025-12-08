namespace Application.Features.Products.DTOs
{
    public sealed record ProductDto(
        Guid Id,
        string Name,
        string Description,
        Decimal Price,
        string Currency,
        int StockQuantity,
        string? Sku,
        bool IsActive,
        bool IsDeleted,
        DateTime CreatedAt,
        DateTime? UpdatedAt
        );
}
