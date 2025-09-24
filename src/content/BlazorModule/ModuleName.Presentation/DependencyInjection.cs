using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModuleName.Application;
using ModuleName.Infrastructure;
using ModuleName.Presentation.EndPoints;
//using ModuleName.Presentation.ModuleAuthShared;

namespace ModuleName.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddModuleName(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        //services
        //    .AddSingleton<IPermissionProvider, PresentationPermissionProvider>();

        // Injection de tous les handlers de l'application
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services
            .AddApplication(configuration, environment)
            .AddInfrastructure(configuration, environment)
            .AddEndPoints(configuration/*voir si utile  , environment*/);

        return services;
    }

    public static WebApplication MapModuleName(this WebApplication builder)
    {
        return builder;
    }

    public static RazorComponentsEndpointConventionBuilder MapModuleNameComponents(this RazorComponentsEndpointConventionBuilder builder)
    {
        builder
            .AddAdditionalAssemblies(typeof(Blazor._Imports).Assembly);

        return builder;
    }
}