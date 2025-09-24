using ApplicationName.Application.Interfaces.Persistence.Repositories;
using ApplicationName.Infrastructure.Persistence;
using ApplicationName.Infrastructure.Persistence.Repositories;
using ApplicationName.SharedKernel.Application.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApplicationName.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services
            .AddScoped(typeof(IApplicationNameRepository<>), typeof(EfRepository<>))
            .AddScoped(typeof(IApplicationNameReadRepository<>), typeof(EfRepository<>));

        services
            .AddScoped<IUnitOfWork, EfUnitOfWork>();

        services
            .AddDbContext<ApplicationNameDbContext>(ServiceLifetime.Scoped);

        // Injection de tous les handlers de l'application
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }
}