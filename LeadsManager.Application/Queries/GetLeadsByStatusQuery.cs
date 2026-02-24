using LeadsManager.Application.DTOs;
using MediatR;

namespace LeadsManager.Application.Queries;

public class GetLeadsByStatusQuery : IRequest<IEnumerable<LeadDto>>
{
    public string Status { get; set; }

    public GetLeadsByStatusQuery(string status)
    {
        Status = status;
    }
}
