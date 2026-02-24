using LeadsManager.Domain.Common;
using LeadsManager.Domain.ValueObjects;

namespace LeadsManager.Domain.Events;

public class LeadCreatedEvent : DomainEvent
{
    public ContactInfo Contact { get; }
    public Location Location { get; }
    public string Category { get; }
    public string Description { get; }
    public Money Price { get; }

    public LeadCreatedEvent(Guid leadId, ContactInfo contact, Location location, 
        string category, string description, Money price) : base(leadId)
    {
        Contact = contact;
        Location = location;
        Category = category;
        Description = description;
        Price = price;
    }
}
