using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Features.Products.DTOs;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Queries.GetActiveProducts
{
    public class GetActiveProductsQueryHandler : IRequestHandler<GetActiveProductsQuery, Result<IReadOnlyList<ProductListDto>>>
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;

        public GetActiveProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<ProductListDto>>> Handle(GetActiveProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.Products.GetActiveProductsAsync(cancellationToken);

            products = [.. products.Where(p => p.IsActive)];

            var productDtos = _mapper.Map<IReadOnlyList<ProductListDto>>(products);

            return Result.Success(productDtos);
        }
    }
}
