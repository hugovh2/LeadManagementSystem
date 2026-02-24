using LeadsManager.Domain.Entities;
using LeadsManager.Domain.Enums;
using LeadsManager.Domain.Repositories;
using LeadsManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LeadsManager.Infrastructure.Repositories;

public class LeadRepository : ILeadRepository
{
    private readonly LeadsDbContext _context;

    public LeadRepository(LeadsDbContext context)
    {
        _context = context;
    }

    public async Task<Lead?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Leads.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<Lead>> GetByStatusAsync(LeadStatus status, CancellationToken cancellationToken = default)
    {
        return await _context.Leads
            .Where(l => l.Status == status)
            .OrderByDescending(l => l.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Lead>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Leads
            .OrderByDescending(l => l.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Lead lead, CancellationToken cancellationToken = default)
    {
        await _context.Leads.AddAsync(lead, cancellationToken);
    }

    public Task UpdateAsync(Lead lead, CancellationToken cancellationToken = default)
    {
        _context.Leads.Update(lead);
        return Task.CompletedTask;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
