using ApplicationName.Application.Persistence.Repositories;
using ApplicationName.Application.Services;
using ApplicationName.Infrastructure.Persistence;
using ApplicationName.Infrastructure.Persistence.Repositories.Base;
using ApplicationName.Infrastructure.Services;
using ApplicationName.SharedKernel.Application.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApplicationName.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services
            .AddPersistence(configuration, environment)
            .AddServices(configuration, environment);

        // Injection de tous les handlers de l'application
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }

    static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services
            .AddScoped(typeof(IApplicationNameRepository<>), typeof(EfRepository<>))
            .AddScoped(typeof(IApplicationNameReadRepository<>), typeof(EfRepository<>))
            //.AddScoped<ISampleRepository, SampleRepository>()
            .AddScoped<IUnitOfWork, EfUnitOfWork>()
            .AddDbContext<ApplicationNameDbContext>(ServiceLifetime.Scoped);

        return services;
    }

    static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services
            .AddScoped<IExternalService, ExternalService>();

        return services;
    }
}