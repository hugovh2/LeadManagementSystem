using LeadsManager.Application.Commands;
using LeadsManager.Application.Common.Interfaces;
using LeadsManager.Domain.Entities;
using LeadsManager.Domain.Repositories;
using LeadsManager.Domain.ValueObjects;
using MediatR;

namespace LeadsManager.Application.Handlers;

public class CreateLeadCommandHandler : IRequestHandler<CreateLeadCommand, Guid>
{
    private readonly ILeadRepository _leadRepository;
    private readonly IEventStore _eventStore;

    public CreateLeadCommandHandler(ILeadRepository leadRepository, IEventStore eventStore)
    {
        _leadRepository = leadRepository;
        _eventStore = eventStore;
    }

    public async Task<Guid> Handle(CreateLeadCommand request, CancellationToken cancellationToken)
    {
        var contact = new ContactInfo(request.FirstName, request.LastName, request.Email, request.PhoneNumber);
        var location = new Location(request.Suburb);
        var price = new Money(request.Price);

        var lead = new Lead(contact, location, request.Category, request.Description, price);

        await _leadRepository.AddAsync(lead, cancellationToken);
        await _leadRepository.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in lead.DomainEvents)
        {
            await _eventStore.SaveEventAsync(domainEvent, cancellationToken);
        }

        return lead.Id;
    }
}
