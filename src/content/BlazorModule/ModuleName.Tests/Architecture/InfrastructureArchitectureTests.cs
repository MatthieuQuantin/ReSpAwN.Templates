using Ardalis.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using NetArchTest.Rules;
using System.Diagnostics.CodeAnalysis;

namespace ModuleName.Tests.Architecture;

[ExcludeFromCodeCoverage]
public class InfrastructureArchitectureTests : BaseArchitectureTests
{
    //TODO : A réactiver si on souhaite controler les dépendances autorisées dans l'infrastructure.
    //[Fact]
    //public void Infrastructure_Should_Only_Have_Dependencies_On_Allowed_Layers()
    //{
    //    var result = Types.InAssembly(InfrastructureAssembly)
    //        .That().ResideInNamespace(InfrastructureAssemblyName)
    //        .Should().OnlyHaveDependenciesOn(
    //            "System",
    //            DomainAssemblyName,
    //            $"{DomainAssemblyName}.*",
    //            ApplicationAssemblyName,
    //            $"{ApplicationAssemblyName}.*",
    //            InfrastructureAssemblyName,
    //            $"{InfrastructureAssemblyName}.*"
    //        )
    //        .GetResult();

    //    Assert.True(result.IsSuccessful, $"{Project} : L'infrastructure ne doit dépendre que de System.*, du domaine, de l'application et d'elle-même.");
    //}

    [Fact]
    public void Infrastructure_Should_Not_Have_Dependencies_On_Presentation()
    {
        var result = Types.InAssembly(InfrastructureAssembly)
            .That().ResideInNamespace(InfrastructureAssemblyName)
            .ShouldNot().HaveDependencyOnAny(PresentationAssemblyName)
            .GetResult();

        Assert.True(result.IsSuccessful, $"{Project} : L'infrastructure ne doit pas dépendre de la couche Presentation.");
    }

    [Fact]
    public void Infrastructure_Configurations_For_Persistence_Should_Be_Sealed_And_Reside_In_Persistence_Configurations_And_Ending_With_Configuration()
    {
        const string ConfigurationsNamespace = "Persistence.Configurations";
        const string ConfigurationSuffix = "Configuration";

        var types = Types.InAssembly(InfrastructureAssembly)
            .That().ImplementInterface(typeof(IEntityTypeConfiguration<>))
            .GetTypes();

        var failingTypes = types
            .Where(t =>
                !t.IsSealed ||
                !t.FullName.StartsWith($"{InfrastructureAssemblyName}.{ConfigurationsNamespace}.") ||
                !t.Name.EndsWith(ConfigurationSuffix))
            .ToList();

        var details = new List<string>();

        failingTypes.ForEach(t =>
        {
            var issues = new List<string>();

            if (!t.IsSealed)
                issues.Add("n'est pas sealed");

            if (!t.FullName.StartsWith($"{InfrastructureAssemblyName}.{ConfigurationsNamespace}."))
                issues.Add($"n'est pas dans un namespace {InfrastructureAssemblyName}.{ConfigurationsNamespace}");

            if (!t.Name.EndsWith(ConfigurationSuffix))
                issues.Add($"ne se termine pas par {ConfigurationSuffix}");

            details.Add($"{t.FullName} - {string.Join(", ", issues)}");
        });

        Assert.True(failingTypes.Count == 0, $"{Project} : Les Configurations doivent être sealed, dans un namespace valide et bien nommés.{Environment.NewLine}{string.Join(Environment.NewLine, details)}");
    }

    [Fact]
    public void Infrastructure_Migrations_For_Persistence_Should_Be_Sealed_And_Reside_In_Persistence_Migrations()
    {
        const string MigrationsNamespace = "Persistence.Migrations";

        var types = Types.InAssembly(InfrastructureAssembly)
            .That().Inherit(typeof(Migration))
            .GetTypes();

        var failingTypes = types
            .Where(t =>
                !t.FullName.StartsWith($"{InfrastructureAssemblyName}.{MigrationsNamespace}."))
            .ToList();

        var details = new List<string>();

        failingTypes.ForEach(t =>
        {
            var issues = new List<string>();

            if (!t.FullName.StartsWith($"{InfrastructureAssemblyName}.{MigrationsNamespace}."))
                issues.Add($"n'est pas dans un namespace {InfrastructureAssemblyName}.{MigrationsNamespace}");

            details.Add($"{t.FullName} - {string.Join(", ", issues)}");
        });

        Assert.True(failingTypes.Count == 0, $"{Project} : Les Migrations doivent être dans un namespace valide.{Environment.NewLine}{string.Join(Environment.NewLine, details)}");
    }

    [Fact]
    public void Infrastructure_Custom_Repositories_Should_Be_Sealed_And_Reside_In_Persistence_Repositories_And_Ending_With_Repository()
    {
        const string CustomRepositoriesNamespace = "Persistence.Repositories";
        const string CustomRepositorySuffix = "Repository";

        var details = new List<string>();

        var types = Types.InAssembly(InfrastructureAssembly)
            .That().ResideInNamespace($"{InfrastructureAssemblyName}.{CustomRepositoriesNamespace}")
            .Or().HaveNameEndingWith(CustomRepositorySuffix)
            .GetTypes();

        var failingTypes = types
            .Where(t =>
                !t.IsSealed ||
                !t.FullName.StartsWith($"{InfrastructureAssemblyName}.{CustomRepositoriesNamespace}.") ||
                !t.Name.EndsWith(CustomRepositorySuffix))
            .ToList();

        failingTypes.ForEach(t =>
        {
            var issues = new List<string>();

            if (!t.IsSealed)
                issues.Add("n'est pas sealed");

            if (!t.FullName.StartsWith($"{InfrastructureAssemblyName}.{CustomRepositoriesNamespace}."))
                issues.Add($"n'est pas dans un namespace {InfrastructureAssemblyName}.{CustomRepositoriesNamespace}");

            if (!t.Name.EndsWith(CustomRepositorySuffix))
                issues.Add($"ne se termine pas par {CustomRepositorySuffix}");

            details.Add($"{t.FullName} - {string.Join(", ", issues)}");
        });

        Assert.True(
            details.Count == 0,
            $"{Project} : Les Custom Repositories doivent être sealed, dans un namespace valide et bien nommés.{Environment.NewLine}{string.Join(Environment.NewLine, details)}");
    }
}