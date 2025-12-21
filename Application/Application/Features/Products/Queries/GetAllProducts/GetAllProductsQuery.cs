using Application.Common.Models;
using Application.Features.Products.DTOs;
using MediatR;

namespace Application.Features.Products.Queries.GetAllProducts
{
    public sealed record GetAllProductsQuery(bool IncludeDeleted = false) : IRequest<Result<IReadOnlyList<ProductListDto>>>
}
