using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Events.Products
{
    public class ProductUpdatedEvent(
        Guid ProductId,
        ProductName NewName,
        string OldName,
        Money NewPrice,
        Money OldPrice) : IDomainEvent
    {
        public Guid EventId { get; } = Guid.NewGuid(); 

        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
