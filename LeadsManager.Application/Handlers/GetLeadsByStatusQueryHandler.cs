using AutoMapper;
using LeadsManager.Application.DTOs;
using LeadsManager.Application.Queries;
using LeadsManager.Domain.Enums;
using LeadsManager.Domain.Repositories;
using MediatR;

namespace LeadsManager.Application.Handlers;

public class GetLeadsByStatusQueryHandler : IRequestHandler<GetLeadsByStatusQuery, IEnumerable<LeadDto>>
{
    private readonly ILeadRepository _leadRepository;
    private readonly IMapper _mapper;

    public GetLeadsByStatusQueryHandler(ILeadRepository leadRepository, IMapper mapper)
    {
        _leadRepository = leadRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LeadDto>> Handle(GetLeadsByStatusQuery request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<LeadStatus>(request.Status, true, out var status))
            throw new ArgumentException($"Invalid status: {request.Status}");

        var leads = await _leadRepository.GetByStatusAsync(status, cancellationToken);
        return _mapper.Map<IEnumerable<LeadDto>>(leads);
    }
}
