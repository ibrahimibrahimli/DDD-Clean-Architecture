using Application.Common.Interfaces;
using Castle.Core.Logging;
using Domain.Events.Product;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Products.EventHandlers
{
    public sealed class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
    {
        readonly ILogger<ProductCreatedEventHandler> _logger;
        private readonly IEmailService _emailService;
        public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
            "Product created: {ProductId} - {ProductName} - Price: {Price} {Currency}",
            notification.ProductId,
            notification.ProductName.Value,
            notification.Price.Amount,
            notification.Price.Currency);
        }
    }
}
