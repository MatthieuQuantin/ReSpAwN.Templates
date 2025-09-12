using ApplicationName.Application.Interfaces.Persistence;
using ApplicationName.Infrastructure.Persistence;
using ApplicationName.SharedKernel.Application.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApplicationName.Infrastructure;

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
        services.AddScoped(typeof(IApplicationNameRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IApplicationNameReadRepository<>), typeof(EfRepository<>));

        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        services.AddDbContext<ApplicationNameDbContext>(ServiceLifetime.Scoped);

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