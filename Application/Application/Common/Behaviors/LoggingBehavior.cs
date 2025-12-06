using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Application.Common.Behaviors
{
    public sealed class LoggingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var stopwatch = Stopwatch.StartNew();

            _logger.LogInformation($"Handling {requestName} - Start");

            try
            {
                var response = await next();

                stopwatch.Stop();

                _logger.LogInformation($"Handled {requestName} - success - Elapsed: {stopwatch.ElapsedMilliseconds}ms");

                return response;
            }
            catch (Exception ex)
            {

                stopwatch.Stop();

                _logger.LogError(ex, $"Handled {requestName} - Failed - Elapsed: {stopwatch.ElapsedMilliseconds}ms");

                throw;
            }
        }
    }
}
