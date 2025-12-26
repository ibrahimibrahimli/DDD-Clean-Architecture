using Application.Common.Models;
using Application.Features.Products.DTOs;
using MediatR;

namespace Application.Features.Products.Commands.CreateProduct
{
    public sealed record CreateProductCommand(
        string Name,
    string Description,
    decimal Price,
    string Currency,
    int StockQuantity,
    string? Sku) : IRequest<Result<ProductDto>>;
}
