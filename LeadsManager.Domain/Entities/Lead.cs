using LeadsManager.Domain.Common;
using LeadsManager.Domain.Enums;
using LeadsManager.Domain.Events;
using LeadsManager.Domain.ValueObjects;

namespace LeadsManager.Domain.Entities;

public class Lead : BaseEntity
{
    public ContactInfo Contact { get; private set; }
    public Location Location { get; private set; }
    public string Category { get; private set; }
    public string Description { get; private set; }
    public Money Price { get; private set; }
    public LeadStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? AcceptedAt { get; private set; }
    public DateTime? DeclinedAt { get; private set; }

    private Lead() { }

    public Lead(ContactInfo contact, Location location, string category, string description, Money price)
    {
        Contact = contact ?? throw new ArgumentNullException(nameof(contact));
        Location = location ?? throw new ArgumentNullException(nameof(location));
        Category = category ?? throw new ArgumentNullException(nameof(category));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Price = price ?? throw new ArgumentNullException(nameof(price));
        Status = LeadStatus.New;
        CreatedAt = DateTime.UtcNow;

        AddDomainEvent(new LeadCreatedEvent(Id, contact, location, category, description, price));
    }

    public void Accept()
    {
        if (Status != LeadStatus.New)
            throw new InvalidOperationException($"Cannot accept lead with status {Status}");

        if (Price.IsGreaterThan(500))
        {
            Price = Price.ApplyDiscount(10);
        }

        Status = LeadStatus.Accepted;
        AcceptedAt = DateTime.UtcNow;

        AddDomainEvent(new LeadAcceptedEvent(Id, Contact, Price, AcceptedAt.Value));
    }

    public void Decline()
    {
        if (Status != LeadStatus.New)
            throw new InvalidOperationException($"Cannot decline lead with status {Status}");

        Status = LeadStatus.Declined;
        DeclinedAt = DateTime.UtcNow;

        AddDomainEvent(new LeadDeclinedEvent(Id, DeclinedAt.Value));
    }
}
