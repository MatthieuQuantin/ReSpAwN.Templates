using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ApplicationName.Application;
using ApplicationName.Infrastructure;
using ApplicationName.Presentation.EndPoints;
//using ApplicationName.Presentation.ModuleAuthShared;

namespace ApplicationName.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationName(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
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

        services
            .AddApplication(configuration, environment)
            .AddInfrastructure(configuration, environment)
            .AddEndPoints(configuration/*voir si utile  , environment*/);

        return services;
    }

    private static IServiceCollection RegisterCommonDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddSingleton<IPermissionProvider, PresentationPermissionProvider>();

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