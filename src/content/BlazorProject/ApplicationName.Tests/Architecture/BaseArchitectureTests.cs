using ApplicationName.Domain.Contracts.SampleAggregate.Events;
using ApplicationName.Domain.SampleAggregate;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ApplicationName.Tests.Architecture;

[ExcludeFromCodeCoverage]
public abstract class BaseArchitectureTests
{
    protected const string Project = "ApplicationName";
    protected const string DomainAssemblyName = $"{Project}.Domain";
    protected const string DomainContractsAssemblyName = $"{DomainAssemblyName}.Contracts";
    protected const string ApplicationAssemblyName = $"{Project}.Application";
    protected const string InfrastructureAssemblyName = $"{Project}.Infrastructure";
    protected const string PresentationAssemblyName = $"{Project}.Presentation";

    protected readonly Assembly DomainAssembly = typeof(Sample).Assembly ?? throw new InvalidOperationException($"Assembly {DomainAssemblyName} not found.");
    protected readonly Assembly DomainContractsAssembly = typeof(SampleCreatedEvent).Assembly ?? throw new InvalidOperationException($"Assembly {DomainContractsAssemblyName} not found.");
    protected readonly Assembly ApplicationAssembly = typeof(ApplicationName.Application.DependencyInjection).Assembly ?? throw new InvalidOperationException($"Assembly {ApplicationAssemblyName} not found.");
    protected readonly Assembly InfrastructureAssembly = typeof(ApplicationName.Infrastructure.DependencyInjection).Assembly ?? throw new InvalidOperationException($"Assembly {InfrastructureAssemblyName} not found.");
    protected readonly Assembly PresentationAssembly = typeof(ApplicationName.Presentation.DependencyInjection).Assembly ?? throw new InvalidOperationException($"Assembly {PresentationAssemblyName} not found.");
}