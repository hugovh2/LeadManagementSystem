using LeadsManager.Application.DTOs;
using MediatR;

namespace LeadsManager.Application.Queries;

public class GetAllLeadsQuery : IRequest<IEnumerable<LeadDto>>
{
}
