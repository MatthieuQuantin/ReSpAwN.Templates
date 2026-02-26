using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectName.Presentation.EndPoints;

internal static class EndPointsRegistration
{
    public static IServiceCollection AddEndPoints(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }

    public static IEndpointRouteBuilder MapEndPoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/application_name", () => "Hello world from application_name !");

        return app;
    }
}