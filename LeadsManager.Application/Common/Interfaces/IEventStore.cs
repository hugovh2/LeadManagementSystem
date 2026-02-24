using LeadsManager.Domain.Common;

namespace LeadsManager.Application.Common.Interfaces;

public interface IEventStore
{
    Task SaveEventAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : DomainEvent;
    Task<IEnumerable<DomainEvent>> GetEventsAsync(Guid aggregateId, CancellationToken cancellationToken = default);
}
