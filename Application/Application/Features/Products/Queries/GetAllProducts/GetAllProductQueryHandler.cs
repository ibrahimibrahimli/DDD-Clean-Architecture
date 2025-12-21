using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Features.Products.DTOs;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductsQuery, Result<IReadOnlyList<ProductListDto>>>
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;

        public GetAllProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<ProductListDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.Products.GetAllAsync(cancellationToken);

            if (!request.IncludeDeleted)
            {
                products = products.Where(p => !p.IsDeleted).ToList();
            }

            var productListDtos = _mapper.Map<IReadOnlyList<ProductListDto>>(products);

            return Result.Success(productListDtos);
        }
    }
}
