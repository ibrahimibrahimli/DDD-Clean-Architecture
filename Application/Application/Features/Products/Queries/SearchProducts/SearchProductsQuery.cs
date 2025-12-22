using Application.Common.Models;
using Application.Features.Products.DTOs;
using MediatR;

namespace Application.Features.Products.Queries.SearchProducts
{
    public sealed record SearchProductsQuery(string SearchTerm) : IRequest<Result<IReadOnlyList<ProductListDto>>>;
}
