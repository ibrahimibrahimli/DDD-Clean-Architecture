using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Features.Products.DTOs;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Queries.GetProductById
{
    public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.Id, cancellationToken);
            if(product is null)
            {
                return Result.Failure<ProductDto>(Error.Product.NotFound(request.Id).Message);
            }

            var productDto = _mapper.Map<ProductDto>(product);

            return Result.Success(productDto);
        }
    }
}
