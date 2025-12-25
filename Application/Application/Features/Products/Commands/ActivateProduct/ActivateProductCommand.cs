using Application.Common.Models;
using MediatR;

namespace Application.Features.Products.Commands.ActivateProduct
{
    public sealed record ActivateProductCommand(Guid Id, bool Activate) : IRequest<Result>;
}
