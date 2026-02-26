using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ProjectName.Design;

public static class DependencyInjection
{
    public static IServiceCollection AddProjectName_Design(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        //services
        //    .AddBootstrapBlazor();

        return services;
    }

    public static IApplicationBuilder UseProjectName_Design(this IApplicationBuilder builder)
    {
        return builder;
    }

    public static IEndpointRouteBuilder MapProjectName_Design(this IEndpointRouteBuilder builder)
    {
        return builder;
    }

    public static RazorComponentsEndpointConventionBuilder MapProjectName_DesignComponents(this RazorComponentsEndpointConventionBuilder builder)
    {
        builder
            .AddAdditionalAssemblies(typeof(_Imports).Assembly);

        return builder;
    }
}