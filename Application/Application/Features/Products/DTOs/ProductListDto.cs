namespace Application.Features.Products.DTOs
{
    public sealed record ProductListDto
    (
        Guid Id,
        string Name,
        decimal Price,
        string Currency,
        int StockQuantity,
        bool IsActive
    );
}
