namespace LeadsManager.Domain.Common;

public abstract class DomainEvent
{
    public Guid EventId { get; }
    public DateTime OccurredOn { get; }
    public Guid AggregateId { get; }

    protected DomainEvent(Guid aggregateId)
    {
        EventId = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
        AggregateId = aggregateId;
    }
}
