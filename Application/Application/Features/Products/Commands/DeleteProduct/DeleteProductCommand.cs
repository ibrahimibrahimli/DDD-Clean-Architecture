using Application.Common.Models;
using MediatR;

namespace Application.Features.Products.Commands.DeleteProduct
{
    public sealed record DeleteProductCommand(Guid Id) : IRequest<Result>;
    
}
