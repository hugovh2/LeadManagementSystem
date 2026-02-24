using AutoMapper;
using LeadsManager.Application.DTOs;
using LeadsManager.Application.Queries;
using LeadsManager.Domain.Repositories;
using MediatR;

namespace LeadsManager.Application.Handlers;

public class GetAllLeadsQueryHandler : IRequestHandler<GetAllLeadsQuery, IEnumerable<LeadDto>>
{
    private readonly ILeadRepository _leadRepository;
    private readonly IMapper _mapper;

    public GetAllLeadsQueryHandler(ILeadRepository leadRepository, IMapper mapper)
    {
        _leadRepository = leadRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LeadDto>> Handle(GetAllLeadsQuery request, CancellationToken cancellationToken)
    {
        var leads = await _leadRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<LeadDto>>(leads);
    }
}
