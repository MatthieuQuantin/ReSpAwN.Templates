using ApplicationName.Domain.SampleAggregate;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ApplicationName.Tests.Architecture;

[ExcludeFromCodeCoverage]
public abstract class BaseArchitectureTests
{
    protected const string Project = "ApplicationName";
    protected const string DomainAssemblyName = $"{Project}.Domain";
    protected const string ApplicationAssemblyName = $"{Project}.Application";
    protected const string InfrastructureAssemblyName = $"{Project}.Infrastructure";
    protected const string PresentationAssemblyName = $"{Project}.Presentation";

    protected readonly Assembly ProjectAssembly = typeof(DependencyInjection).Assembly;
    protected readonly Assembly ProjectContractsAssembly = typeof(SampleId).Assembly;
}