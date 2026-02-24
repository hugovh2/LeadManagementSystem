using LeadsManager.Application.Common.Interfaces;
using LeadsManager.Domain.Common;
using LeadsManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LeadsManager.Infrastructure.EventSourcing;

public class EventStore : IEventStore
{
    private readonly LeadsDbContext _context;

    public EventStore(LeadsDbContext context)
    {
        _context = context;
    }

    public async Task SaveEventAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) 
        where TEvent : DomainEvent
    {
        var storedEvent = new StoredEvent
        {
            Id = Guid.NewGuid(),
            EventType = @event.GetType().Name,
            AggregateId = @event.AggregateId,
            Data = JsonConvert.SerializeObject(@event),
            OccurredOn = @event.OccurredOn
        };

        await _context.Events.AddAsync(storedEvent, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<DomainEvent>> GetEventsAsync(Guid aggregateId, CancellationToken cancellationToken = default)
    {
        var storedEvents = await _context.Events
            .Where(e => e.AggregateId == aggregateId)
            .OrderBy(e => e.OccurredOn)
            .ToListAsync(cancellationToken);

        var events = new List<DomainEvent>();
        
        foreach (var storedEvent in storedEvents)
        {
            var eventType = Type.GetType(storedEvent.EventType);
            if (eventType != null)
            {
                var domainEvent = JsonConvert.DeserializeObject(storedEvent.Data, eventType) as DomainEvent;
                if (domainEvent != null)
                {
                    events.Add(domainEvent);
                }
            }
        }

        return events;
    }
}
