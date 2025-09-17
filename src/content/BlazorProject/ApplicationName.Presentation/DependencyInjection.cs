using ApplicationName.Application;
using ApplicationName.Infrastructure;
using ApplicationName.Presentation.Blazor;
using ApplicationName.Presentation.EndPoints;
//using ApplicationName.Presentation.ModuleAuthShared;

namespace ApplicationName.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationName(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
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

        //services
        //    .AddModuleName(configuration, environment); // Module Auth Shared

        return services;
    }

    public static WebApplication MapApplicationName(this WebApplication builder)
    {
        #region Pour l'authentification et l'autorisation des utilisateurs (Identity)

        //builder
        //    .UseAuthentication()
        //    .UseAuthorization();

        #endregion

        builder
            .UseAntiforgery();

        builder
            .MapStaticAssets();

        //builder
        //    .MapModuleName();

        builder
            .MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();
            //.AddModuleNameComponents();

        return builder;
    }
}