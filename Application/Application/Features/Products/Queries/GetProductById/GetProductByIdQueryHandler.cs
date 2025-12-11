using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Features.Products.DTOs;
using MediatR;

namespace Application.Features.Products.Queries.GetProductById
{
    //public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
    //{
    //    readonly IUnitOfWork _unitOfWork;

    //    public GetProductByIdQueryHandler(IUnitOfWork unitOfWork)
    //    {
    //        _unitOfWork = unitOfWork;
    //    }

    //    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    //    {
    //        //var product = await _unitOfWork.
    //    }
    //}
}
