using LeadsManager.Application.Common.Interfaces;
using LeadsManager.Domain.Repositories;
using LeadsManager.Infrastructure.EventSourcing;
using LeadsManager.Infrastructure.Persistence;
using LeadsManager.Infrastructure.Repositories;
using LeadsManager.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LeadsManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LeadsDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ILeadRepository, LeadRepository>();
        services.AddScoped<IEventStore, EventStore>();
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}
