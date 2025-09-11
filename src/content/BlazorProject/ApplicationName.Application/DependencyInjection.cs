using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using ApplicationName.Application.ModuleAuthShared;

namespace ApplicationName.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
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
        //services.AddSingleton<IPermissionProvider, ApplicationPermissionProvider>();

        // Injection de tous les validateurs de l'application
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

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