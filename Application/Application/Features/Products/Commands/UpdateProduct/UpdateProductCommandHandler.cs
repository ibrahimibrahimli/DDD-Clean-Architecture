using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Features.Products.DTOs;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<ProductDto>>
    {
        readonly IMapper _mapper;
        readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _unitOfWork.Products.GetByIdAsync(request.Id, cancellationToken);
                if (product is null)
                {
                    return Result.Failure<ProductDto>(Error.Product.NotFound(request.Id).Message);
                }

                if (product.IsDeleted)
                {
                    return Result.Failure<ProductDto>(
                        Error.Product.AlreadyDeleted.Message);
                }

                product.UpdateDetails(request.Name,
                    request.Description,
                    request.Price,
                    request.Currency);

                await _unitOfWork.Products.UpdateAsync(product, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var productDto = _mapper.Map<ProductDto>(product);

                return Result.Success(productDto);
            }
            catch (ArgumentException ex)
            {
                return Result.Failure<ProductDto>(ex.Message);
            }
            catch (Exception ex)
            {
                return Result.Failure<ProductDto>($"An error occurred while updating the product: {ex.Message}");
                throw;
            }
        }
    }
}
