using LeadsManager.Domain.Entities;
using LeadsManager.Domain.Enums;

namespace LeadsManager.Domain.Repositories;

public interface ILeadRepository
{
    Task<Lead?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Lead>> GetByStatusAsync(LeadStatus status, CancellationToken cancellationToken = default);
    Task<IEnumerable<Lead>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Lead lead, CancellationToken cancellationToken = default);
    Task UpdateAsync(Lead lead, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
