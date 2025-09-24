using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using ApplicationName.Application.ModuleAuthShared;

namespace ApplicationName.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        //services
        //    .AddSingleton<IPermissionProvider, ApplicationPermissionProvider>();

        // Injection de tous les validateurs de l'application
        services
            .AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        // Injection de tous les handlers de l'application
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }
}