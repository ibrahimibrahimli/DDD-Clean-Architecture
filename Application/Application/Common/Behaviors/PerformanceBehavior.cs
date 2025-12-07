using Castle.Core.Logging;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Application.Common.Behaviors
{
    public sealed class PerformanceBehavior<Trequest, TResponse>
        : IPipelineBehavior<Trequest, TResponse>
        where Trequest : IRequest<TResponse>
    {
        readonly ILogger<PerformanceBehavior<Trequest, TResponse>> _logger;
        readonly Stopwatch _timer;

        public PerformanceBehavior(ILogger<PerformanceBehavior<Trequest, TResponse>> logger)
        {
            _logger = logger;
            _timer = new Stopwatch();
        }
        public async Task<TResponse> Handle(Trequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500)
            {
                var requestName = typeof(Trequest).Name;

                _logger.LogWarning($"Long running request: {requestName} - {elapsedMilliseconds} milliseconds")
            }

            return response;
        }
    }
}
