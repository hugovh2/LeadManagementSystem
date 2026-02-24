namespace LeadsManager.Infrastructure.Persistence;

public class StoredEvent
{
    public Guid Id { get; set; }
    public string EventType { get; set; } = string.Empty;
    public Guid AggregateId { get; set; }
    public string Data { get; set; } = string.Empty;
    public DateTime OccurredOn { get; set; }
}
