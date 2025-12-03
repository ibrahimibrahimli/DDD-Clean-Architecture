using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Events.Products
{
    public class ProductOutOfStockEvent(
        Guid ProductId,
        ProductName ProductName) : IDomainEvent
    {
        public Guid EventId => Guid.NewGuid();

        public DateTime OccurredOn => DateTime.UtcNow;
    }
}
