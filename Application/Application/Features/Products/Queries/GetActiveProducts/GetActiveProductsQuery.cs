using Application.Common.Models;
using Application.Features.Products.DTOs;
using MediatR;

namespace Application.Features.Products.Queries.GetActiveProducts
{
    public sealed record GetActiveProductsQuery : IRequest<Result<IReadOnlyList<ProductListDto>>>;
}
