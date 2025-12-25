using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Features.Products.Commands.ActivateProduct
{
    public sealed class ActivateProductCommandHandler : IRequestHandler<ActivateProductCommand, Result>
    {
        readonly IUnitOfWork _unitOfWork;

        public ActivateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ActivateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.Id, cancellationToken);

            if (product is null)
            {
                return Result.Failure(Error.Product.NotFound(request.Id).Message);
            }

            if (request.Activate)
                product.Activate();
            else
                product.Deactivate();

            await _unitOfWork.Products.UpdateAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
