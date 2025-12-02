using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Events.Products
{
    public class ProductDeletedEvent(
        Guid ProductId,
        ProductName ProductName) : IDomainEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();

        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
