using Domain.Abstractions;
using System.Collections.ObjectModel;

namespace Domain.Primitives
{
    public class BaseEntity<TId> : IEntity
    {
        public TId Id { get; protected set; } = default!;
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => new ReadOnlyCollection<IDomainEvent>(_domainEvents);
    }
}
