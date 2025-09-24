using ApplicationName.SharedKernel.Application.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModuleName.Application.Interfaces.Persistence.Repositories;
using ModuleName.Infrastructure.Persistence;
using ModuleName.Infrastructure.Persistence.Repositories;

namespace ModuleName.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services
            .AddScoped(typeof(IModuleNameRepository<>), typeof(EfRepository<>))
            .AddScoped(typeof(IModuleNameReadRepository<>), typeof(EfRepository<>))
            .AddScoped(typeof(IPersonRepository), typeof(PersonRepository));

        services
            .AddScoped<IUnitOfWork, EfUnitOfWork>();

        services
            .AddDbContext<ModuleNameDbContext>(ServiceLifetime.Scoped);

        // Injection de tous les handlers de l'application
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }
}