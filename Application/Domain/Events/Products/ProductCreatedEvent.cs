using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Events.Product
{
    public sealed record ProductCreatedEvent(
        Guid ProductId,
        ProductName ProductName,
        Money Price
        ) : IDomainEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();

        public DateTime OccurredOn {  get; } = DateTime.UtcNow;
    }
}
