using Application.Common.Models;
using Application.Features.Products.DTOs;
using MediatR;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public sealed record UpdateProductCommand(Guid Id,
    string Name,
    string Description,
    decimal Price,
    string Currency) : IRequest<Result<ProductDto>>;
}
