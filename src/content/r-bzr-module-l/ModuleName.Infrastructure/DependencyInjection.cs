using ApplicationName.SharedKernel.Application.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModuleName.Application.Persistence.Repositories;
using ModuleName.Infrastructure.Persistence;
using ModuleName.Infrastructure.Persistence.Repositories;
using ModuleName.Infrastructure.Persistence.Repositories.Base;

namespace ModuleName.Infrastructure;

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
            .AddScoped(typeof(IModuleNameRepository<>), typeof(EfRepository<>))
            .AddScoped(typeof(IModuleNameReadRepository<>), typeof(EfRepository<>))
            .AddScoped<IPersonRepository, PersonRepository>()
            .AddScoped<IUnitOfWork, EfUnitOfWork>()
            .AddDbContext<ModuleNameDbContext>(ServiceLifetime.Scoped);

        return services;
    }

    static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        //services
        //    .AddScoped<IExternalService, ExternalService>();

        return services;
    }
}