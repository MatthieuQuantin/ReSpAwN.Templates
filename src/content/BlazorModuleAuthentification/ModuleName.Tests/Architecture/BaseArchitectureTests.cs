using ModuleName.Domain.ApplicationUserAggregate;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ModuleName.Tests.Architecture;

[ExcludeFromCodeCoverage]
public abstract class BaseArchitectureTests
{
    protected const string Project = "ModuleName";
    protected const string DomainAssemblyName = $"{Project}.Domain";
    protected const string DomainContractsAssemblyName = $"{DomainAssemblyName}.Contracts";
    protected const string ApplicationAssemblyName = $"{Project}.Application";
    protected const string InfrastructureAssemblyName = $"{Project}.Infrastructure";
    protected const string PresentationAssemblyName = $"{Project}.Presentation";

    protected readonly Assembly DomainAssembly = typeof(ApplicationUser).Assembly ?? throw new InvalidOperationException($"Assembly {DomainAssemblyName} not found.");
    //protected readonly Assembly DomainContractsAssembly = typeof(PersonCreatedEvent).Assembly ?? throw new InvalidOperationException($"Assembly {DomainContractsAssemblyName} not found.");
    protected readonly Assembly ApplicationAssembly = typeof(ModuleName.Application.DependencyInjection).Assembly ?? throw new InvalidOperationException($"Assembly {ApplicationAssemblyName} not found.");
    protected readonly Assembly InfrastructureAssembly = typeof(ModuleName.Infrastructure.DependencyInjection).Assembly ?? throw new InvalidOperationException($"Assembly {InfrastructureAssemblyName} not found.");
    protected readonly Assembly PresentationAssembly = typeof(ModuleName.Presentation.DependencyInjection).Assembly ?? throw new InvalidOperationException($"Assembly {PresentationAssemblyName} not found.");
}