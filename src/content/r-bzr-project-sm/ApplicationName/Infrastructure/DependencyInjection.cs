using ApplicationName.Application.Persistence.Repositories;
using ApplicationName.Application.Services;
using ApplicationName.Infrastructure.Persistence;
using ApplicationName.Infrastructure.Persistence.Repositories;
using ApplicationName.Infrastructure.Persistence.Repositories.Base;
using ApplicationName.Infrastructure.Services;
using ApplicationName.SharedKernel.Application.Persistence;

namespace ApplicationName.Infrastructure;

internal static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services
            .AddScoped(typeof(IApplicationNameRepository<>), typeof(EfRepository<>))
            .AddScoped(typeof(IApplicationNameReadRepository<>), typeof(EfRepository<>))
            .AddScoped<ISampleRepository, SampleRepository>()
            .AddScoped<IExternalService, ExternalService>();

        services
            .AddScoped<IUnitOfWork, EfUnitOfWork>();

        services
            .AddDbContext<ApplicationNameDbContext>(ServiceLifetime.Scoped);

        // Injection de tous les handlers de l'application
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }
}