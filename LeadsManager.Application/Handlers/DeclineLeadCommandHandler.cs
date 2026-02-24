using LeadsManager.Application.Commands;
using LeadsManager.Application.Common.Interfaces;
using LeadsManager.Domain.Repositories;
using MediatR;

namespace LeadsManager.Application.Handlers;

public class DeclineLeadCommandHandler : IRequestHandler<DeclineLeadCommand, Unit>
{
    private readonly ILeadRepository _leadRepository;
    private readonly IEventStore _eventStore;

    public DeclineLeadCommandHandler(ILeadRepository leadRepository, IEventStore eventStore)
    {
        _leadRepository = leadRepository;
        _eventStore = eventStore;
    }

    public async Task<Unit> Handle(DeclineLeadCommand request, CancellationToken cancellationToken)
    {
        var lead = await _leadRepository.GetByIdAsync(request.LeadId, cancellationToken);
        if (lead == null)
            throw new InvalidOperationException($"Lead with ID {request.LeadId} not found");

        lead.Decline();

        await _leadRepository.UpdateAsync(lead, cancellationToken);
        await _leadRepository.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in lead.DomainEvents)
        {
            await _eventStore.SaveEventAsync(domainEvent, cancellationToken);
        }

        return Unit.Value;
    }
}
