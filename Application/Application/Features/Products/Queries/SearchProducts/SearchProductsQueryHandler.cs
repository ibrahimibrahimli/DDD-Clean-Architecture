using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Features.Products.DTOs;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Queries.SearchProducts
{
    public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, Result<IReadOnlyList<ProductListDto>>>
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        public SearchProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<ProductListDto>>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.SearchTerm))
            {
                return Result.Failure<IReadOnlyList<ProductListDto>>(
                "Search term cannot be empty");
            }

            var products = await _unitOfWork.Products.SearchByNameAsync(request.SearchTerm);

            var productsDto = _mapper.Map<IReadOnlyList<ProductListDto>>(products);

            return Result.Success(productsDto);
        }
    }
}
