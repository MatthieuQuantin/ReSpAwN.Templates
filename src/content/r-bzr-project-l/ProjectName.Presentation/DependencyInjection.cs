using ProjectName.Application;
using ProjectName.Design;
using ProjectName.Infrastructure;
using ProjectName.Presentation.Blazor;
using ProjectName.Presentation.EndPoints;

namespace ProjectName.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddProjectName(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        #region Pour l'authentification et l'autorisation des utilisateurs (Identity)

        //services.AddAuthentication();
        //services.AddAuthorization();

        #endregion

        services
            .AddRazorComponents()
            .AddInteractiveServerComponents();

        //services
        //    .AddSingleton<IPermissionProvider, PresentationPermissionProvider>();

        // Injection de tous les handlers de l'application
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services
            .AddApplication(configuration, environment)
            .AddInfrastructure(configuration, environment)
            .AddEndPoints(configuration/*voir si utile  , environment*/);

        services
            .AddProjectName_Design(configuration, environment);
            //.AddModuleName(configuration, environment); // Module Auth Shared

        return services;
    }

    public static IApplicationBuilder UseProjectName(this IApplicationBuilder builder)
    {
        builder
            .UseRouting();

        #region Pour l'authentification et l'autorisation des utilisateurs (Identity)

        //builder
        //    .UseAuthentication()
        //    .UseAuthorization();

        #endregion

        builder
            .UseAntiforgery();

        builder
            .UseProjectName_Design();
            //.UseModuleName();

        return builder;
    }

    public static IEndpointRouteBuilder MapProjectName(this IEndpointRouteBuilder builder)
    {
        builder
            .MapStaticAssets();

        builder
            .MapProjectName_Design();
            //.MapModuleName();

        builder
            .MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .MapProjectName_DesignComponents();
            //.MapModuleNameComponents();

        return builder;
    }
}