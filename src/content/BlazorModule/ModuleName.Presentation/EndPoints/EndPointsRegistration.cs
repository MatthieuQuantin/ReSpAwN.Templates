using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ModuleName.Presentation.EndPoints;

internal static class EndPointsRegistration
{
    public static IServiceCollection AddEndPoints(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }

    public static IEndpointRouteBuilder MapEndPoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/module_name", () => "Hello world from module_name !");

        return app;
    }
}