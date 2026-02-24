using MediatR;

namespace LeadsManager.Application.Commands;

public class DeclineLeadCommand : IRequest<Unit>
{
    public Guid LeadId { get; set; }

    public DeclineLeadCommand(Guid leadId)
    {
        LeadId = leadId;
    }
}
