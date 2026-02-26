using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectName.Application.Persistence.Repositories;
using ProjectName.Application.Services;
using ProjectName.Infrastructure.Persistence;
using ProjectName.Infrastructure.Persistence.Repositories;
using ProjectName.Infrastructure.Persistence.Repositories.Base;
using ProjectName.Infrastructure.Services;
using ProjectName.SharedKernel.Application.Persistence;

namespace ProjectName.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services
            .AddPersistence(configuration, environment)
            .AddServices(configuration, environment);

        // Injection de tous les handlers de l'application
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }

    static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services
            .AddScoped(typeof(IProjectNameRepository<>), typeof(EfRepository<>))
            .AddScoped(typeof(IProjectNameReadRepository<>), typeof(EfRepository<>))
            .AddScoped<IPersonRepository, PersonRepository>()
            .AddScoped<IUnitOfWork, EfUnitOfWork>()
            .AddDbContext<ProjectNameDbContext>(ServiceLifetime.Scoped);

        return services;
    }

    static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services
            .AddScoped<IExternalService, ExternalService>();

        return services;
    }
}