using LeadsManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeadsManager.Infrastructure.Persistence;

public class LeadsDbContext : DbContext
{
    public LeadsDbContext(DbContextOptions<LeadsDbContext> options) : base(options)
    {
    }

    public DbSet<Lead> Leads { get; set; } = null!;
    public DbSet<StoredEvent> Events { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Lead>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.OwnsOne(e => e.Contact, contact =>
            {
                contact.Property(c => c.FirstName).HasMaxLength(100).IsRequired();
                contact.Property(c => c.LastName).HasMaxLength(100).IsRequired();
                contact.Property(c => c.Email).HasMaxLength(255).IsRequired();
                contact.Property(c => c.PhoneNumber).HasMaxLength(50);
            });

            entity.OwnsOne(e => e.Location, location =>
            {
                location.Property(l => l.Suburb).HasMaxLength(200).IsRequired();
                location.Property(l => l.PostalCode).HasMaxLength(20);
            });

            entity.OwnsOne(e => e.Price, price =>
            {
                price.Property(p => p.Amount).HasColumnType("decimal(18,2)").IsRequired();
                price.Property(p => p.Currency).HasMaxLength(10).IsRequired();
            });

            entity.Property(e => e.Category).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(1000).IsRequired();
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();

            entity.Ignore(e => e.DomainEvents);
        });

        modelBuilder.Entity<StoredEvent>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.EventType).HasMaxLength(255).IsRequired();
            entity.Property(e => e.AggregateId).IsRequired();
            entity.Property(e => e.Data).IsRequired();
            entity.Property(e => e.OccurredOn).IsRequired();
            entity.HasIndex(e => e.AggregateId);
        });
    }
}
