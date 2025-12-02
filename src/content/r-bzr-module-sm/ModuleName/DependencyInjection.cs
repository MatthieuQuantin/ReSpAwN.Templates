using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModuleName.Application;
using ModuleName.Infrastructure;
using ModuleName.Presentation.EndPoints;
//using ModuleName.Presentation.ModuleAuthShared;

namespace ModuleName;

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

    public static IApplicationBuilder UseModuleName(this IApplicationBuilder builder)
    {
        return builder;
    }

    public static IEndpointRouteBuilder MapModuleName(this IEndpointRouteBuilder builder)
    {
        return builder;
    }

    public static RazorComponentsEndpointConventionBuilder MapModuleNameComponents(this RazorComponentsEndpointConventionBuilder builder)
    {
        builder
            .AddAdditionalAssemblies(typeof(Presentation.Blazor._Imports).Assembly);

        return builder;
    }
}