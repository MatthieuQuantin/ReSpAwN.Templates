using ApplicationName.SharedKernel.Application.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModuleName.Application.Interfaces.Persistence;
using ModuleName.Infrastructure.Persistence;
using ModuleName.Infrastructure.Persistence.Repositories;

namespace ModuleName.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services.RegisterCommonDependencies(configuration);

        if (environment.IsDevelopment())
        {
            services.RegisterDevelopmentOnlyDependencies(configuration);
        }
        else
        {
            services.RegisterProductionOnlyDependencies(configuration);
        }

        return services;
    }

    private static IServiceCollection RegisterCommonDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IModuleNameRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IModuleNameReadRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IPersonRepository), typeof(PersonRepository));

        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        services.AddDbContext<ModuleNameDbContext>(ServiceLifetime.Scoped);

        // Injection de tous les handlers de l'application
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }

    private static IServiceCollection RegisterDevelopmentOnlyDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO: Add development only services

        return services;
    }

    private static IServiceCollection RegisterProductionOnlyDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO: Add production only services

        return services;
    }
}