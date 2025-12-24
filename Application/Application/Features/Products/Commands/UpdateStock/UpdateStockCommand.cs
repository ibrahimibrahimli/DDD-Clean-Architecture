using Application.Common.Models;
using MediatR;

namespace Application.Features.Products.Commands.UpdateStock
{
    public sealed record UpdateStockCommand(Guid ProductId, int Quantity) : IRequest<Result>;
}
