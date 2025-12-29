using Application.Common.Interfaces;
using Domain.Common;
using MediatR;

namespace Infrustructure.Services
{
    public sealed class DomainEventDispatcher : IDomainEventDispatcher
    {
        readonly IPublisher _publisher;

        public DomainEventDispatcher(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }
        }
    }
}
