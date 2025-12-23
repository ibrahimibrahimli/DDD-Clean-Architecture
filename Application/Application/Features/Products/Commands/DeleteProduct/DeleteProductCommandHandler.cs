using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.Id);

            if(product is null)
                return Result.Failure(Error.Product.NotFound(request.Id).Message);
            

            if(product.IsDeleted)
                return Result.Failure(Error.Product.AlreadyDeleted.Message);

            product.Delete();
            await _unitOfWork.Products.UpdateAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
    }
}
