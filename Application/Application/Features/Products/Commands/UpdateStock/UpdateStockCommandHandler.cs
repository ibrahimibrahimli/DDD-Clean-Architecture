using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Features.Products.Commands.UpdateStock
{
    public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, Result>
    {
        readonly IUnitOfWork _unitOfWork;

        public UpdateStockCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId, cancellationToken);

                if (product is null)
                {
                    return Result.Failure(Error.Product.NotFound(request.ProductId).Message);
                }

                product.UpdateStock(request.Quantity);

                await _unitOfWork.Products.UpdateAsync(product, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result.Success(product);
            }
            catch (ArgumentException ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
