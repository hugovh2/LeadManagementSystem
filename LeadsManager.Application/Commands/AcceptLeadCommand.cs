using MediatR;

namespace LeadsManager.Application.Commands;

public class AcceptLeadCommand : IRequest<Unit>
{
    public Guid LeadId { get; set; }

    public AcceptLeadCommand(Guid leadId)
    {
        LeadId = leadId;
    }
}
