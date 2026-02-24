using LeadsManager.Domain.Common;

namespace LeadsManager.Domain.Events;

public class LeadDeclinedEvent : DomainEvent
{
    public DateTime DeclinedAt { get; }

    public LeadDeclinedEvent(Guid leadId, DateTime declinedAt) : base(leadId)
    {
        DeclinedAt = declinedAt;
    }
}
