
using Application.Common.Interfaces;
using MediatR;

namespace Application.Common.Behaviors
{
    public sealed class UnitOfWorkBehavior<TRequset, TResponse>
        : IPipelineBehavior<TRequset, TResponse>
        where TRequset : IRequest<TResponse>
    {
        readonly IUnitOfWork _unitOfWork;
        public UnitOfWorkBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<TResponse> Handle(TRequset request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //if is this a query dont need transaction
            if (typeof(TRequset).Name.EndsWith("Query"))
            {
                return await next();
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                var response = await next();

                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return response;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollBackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
