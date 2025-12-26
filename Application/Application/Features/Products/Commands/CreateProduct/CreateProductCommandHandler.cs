using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Features.Products.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductDto>>
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Sku))
                {
                    var existingProduct = await _unitOfWork.Products.GetBySkuAsync(request.Sku, cancellationToken);

                    if (existingProduct is not null)
                    {
                        return Result.Failure<ProductDto>(
                        Error.Product.SkuAlreadyExists(request.Sku).Message);
                    }
                }

                var product = Product.Create(
                    request.Name,
                    request.Description,
                    request.Price,
                    request.StockQuantity,
                    request.Sku,
                    request.Currency);

                await _unitOfWork.Products.AddAsync(product, cancellationToken);
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
                return Result.Failure<ProductDto>($"An error occurred while creating the product: {ex.Message}");
            }
        }
    }
}
