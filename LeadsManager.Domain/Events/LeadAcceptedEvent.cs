using LeadsManager.Domain.Common;
using LeadsManager.Domain.ValueObjects;

namespace LeadsManager.Domain.Events;

public class LeadAcceptedEvent : DomainEvent
{
    public ContactInfo Contact { get; }
    public Money FinalPrice { get; }
    public DateTime AcceptedAt { get; }

    public LeadAcceptedEvent(Guid leadId, ContactInfo contact, Money finalPrice, DateTime acceptedAt) 
        : base(leadId)
    {
        Contact = contact;
        FinalPrice = finalPrice;
        AcceptedAt = acceptedAt;
    }
}
