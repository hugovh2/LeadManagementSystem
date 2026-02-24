using LeadsManager.Application.Commands;
using LeadsManager.Application.Common.Interfaces;
using LeadsManager.Domain.Repositories;
using MediatR;

namespace LeadsManager.Application.Handlers;

public class AcceptLeadCommandHandler : IRequestHandler<AcceptLeadCommand, Unit>
{
    private readonly ILeadRepository _leadRepository;
    private readonly IEventStore _eventStore;
    private readonly IEmailService _emailService;

    public AcceptLeadCommandHandler(
        ILeadRepository leadRepository, 
        IEventStore eventStore,
        IEmailService emailService)
    {
        _leadRepository = leadRepository;
        _eventStore = eventStore;
        _emailService = emailService;
    }

    public async Task<Unit> Handle(AcceptLeadCommand request, CancellationToken cancellationToken)
    {
        var lead = await _leadRepository.GetByIdAsync(request.LeadId, cancellationToken);
        if (lead == null)
            throw new InvalidOperationException($"Lead with ID {request.LeadId} not found");

        lead.Accept();

        await _leadRepository.UpdateAsync(lead, cancellationToken);
        await _leadRepository.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in lead.DomainEvents)
        {
            await _eventStore.SaveEventAsync(domainEvent, cancellationToken);
        }

        var emailBody = $@"
🎉 NEW LEAD ACCEPTED - ACTION REQUIRED

👤 CONTACT INFORMATION:
   Name:     {lead.Contact.FullName}
   Email:    {lead.Contact.Email}
   Phone:    {lead.Contact.PhoneNumber}
   Location: {lead.Location.Suburb}

🔧 JOB INFORMATION:
   Category:    {lead.Category}
   Description: {lead.Description}
   Lead ID:     {lead.Id}
   Accepted:    {DateTime.Now:yyyy-MM-dd HH:mm:ss}

💰 FINAL PRICE:
   {lead.Price} (Lead Invitation Value)

⚡ NEXT STEPS:
   1. Contact the customer within 24 hours
   2. Schedule a consultation or site visit
   3. Prepare a detailed quote if needed
   4. Update the lead status in the system

-----------------------------------------------------------
� This email was generated automatically by LeadsManager System
⚠️  This is a simulated email for development purposes
";

        await _emailService.SendEmailAsync("vendas@test.com", "🎉 New Lead Accepted - Action Required | LeadsManager", emailBody, cancellationToken);

        return Unit.Value;
    }
}
