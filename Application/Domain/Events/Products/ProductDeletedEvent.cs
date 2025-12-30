using Domain.Common;
using Domain.ValueObjects;
using MediatR;

namespace Domain.Events.Products
{
    public class ProductDeletedEvent(
        Guid ProductId,
        ProductName ProductName) : IDomainEvent, INotification
    {
        public Guid EventId { get; } = Guid.NewGuid();

        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
