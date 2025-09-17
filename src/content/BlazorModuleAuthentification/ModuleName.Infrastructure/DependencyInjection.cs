using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModuleName.Domain.ApplicationRoleAggregate;
using ModuleName.Domain.ApplicationUserAggregate;
using ModuleName.Infrastructure.Persistence;

namespace ModuleName.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services
            .AddDbContext<ModuleNameDbContext>(ServiceLifetime.Scoped);

        services
            .AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // Politique de connexion
                options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedEmail = true;

                // Politique MDP (exemple)
                options.Password.RequiredLength = 12;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;

                // Token providers utilisés par les flux e-mail/2FA
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Tokens.ChangeEmailTokenProvider = TokenOptions.DefaultEmailProvider;
                // (les providers “par défaut” gèrent reset MDP, 2FA, etc.)
            })
            .AddEntityFrameworkStores<ModuleNameDbContext>()
            .AddDefaultTokenProviders();// reset MDP, 2FA, email, etc.

        services
            .ConfigureApplicationCookie(o =>
            {
                o.SlidingExpiration = true;
                o.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                o.Cookie.HttpOnly = true;
            });

        // Injection de tous les handlers de l'application
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }
}