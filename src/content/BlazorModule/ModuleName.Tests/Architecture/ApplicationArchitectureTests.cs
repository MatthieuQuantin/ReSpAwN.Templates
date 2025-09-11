using Ardalis.SharedKernel;
using FluentValidation;
using NetArchTest.Rules;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ModuleName.Tests.Architecture;

[ExcludeFromCodeCoverage]
public class ApplicationArchitectureTests : BaseArchitectureTests
{
    //TODO : A réactiver si on souhaite controler les dépendances autorisées dans l'application.
    //[Fact]
    //public void Application_Should_Only_Have_Dependencies_On_System_And_Domain()
    //{
    //    var result = Types.InAssembly(ApplicationAssembly)
    //        .That().ResideInNamespace(ApplicationAssemblyName)
    //        .Should().OnlyHaveDependenciesOn("System", DomainAssemblyName, $"{DomainAssemblyName}.*", ApplicationAssemblyName, $"{ApplicationAssemblyName}.*")
    //        .GetResult();

    //    Assert.True(result.IsSuccessful, $"{Project} : L'application ne doit dépendre que de System.*, du domaine et d'elle-même.");
    //}

    [Fact]
    public void Application_Should_Not_Have_Dependencies_On_Infrastructure_Or_Presentation()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That().ResideInNamespace(ApplicationAssemblyName)
            .ShouldNot().HaveDependencyOnAny(InfrastructureAssemblyName, PresentationAssemblyName)
            .GetResult();

        Assert.True(result.IsSuccessful, $"{Project} : L'application ne doit pas dépendre des couches Infrastructure ou Presentation.");
    }

    [Fact]
    public void Application_Commands_Should_Be_Sealed_And_Reside_In_Features_And_Ending_With_Command()
    {
        const string CommandsNamespace = "Features";
        const string CommandSuffix = "Command";

        var details = new List<string>();

        var types = Types.InAssembly(ApplicationAssembly)
            .That().ImplementInterface(typeof(ICommand<>))
            .GetTypes();

        var failingTypes = types
            .Where(t =>
                !t.IsSealed ||
                !t.FullName.StartsWith($"{ApplicationAssemblyName}.{CommandsNamespace}.") ||
                !t.Name.EndsWith(CommandSuffix))
            .ToList();

        failingTypes.ForEach(t =>
        {
            var issues = new List<string>();

            if (!t.IsSealed)
                issues.Add("n'est pas sealed");

            if (!t.FullName.StartsWith($"{ApplicationAssemblyName}.{CommandsNamespace}."))
                issues.Add($"n'est pas dans un namespace {ApplicationAssemblyName}.{CommandsNamespace}");

            if (!t.Name.EndsWith(CommandSuffix))
                issues.Add($"ne se termine pas par {CommandSuffix}");

            details.Add($"{t.FullName} - {string.Join(", ", issues)}");
        });

        Assert.True(
            details.Count == 0,
            $"{Project} : Les Commands doivent respecter les conventions de nommage.{Environment.NewLine}{string.Join(Environment.NewLine, details)}");
    }

    [Fact]
    public void Application_Queries_Should_Be_Sealed_And_Reside_In_Features_And_Ending_With_Query()
    {
        const string QueriesNamespace = "Features";
        const string QuerySuffix = "Query";

        var details = new List<string>();

        var types = Types.InAssembly(ApplicationAssembly)
            .That().ImplementInterface(typeof(IQuery<>))
            .GetTypes();

        var failingTypes = types
            .Where(t =>
                !t.IsSealed ||
                !t.FullName.StartsWith($"{ApplicationAssemblyName}.{QueriesNamespace}.")
                || !t.Name.EndsWith(QuerySuffix))
            .ToList();

        failingTypes.ForEach(t =>
        {
            var issues = new List<string>();

            if (!t.IsSealed)
                issues.Add("n'est pas sealed");

            if (!t.FullName.StartsWith($"{ApplicationAssemblyName}.{QueriesNamespace}."))
                issues.Add($"n'est pas dans un namespace {ApplicationAssemblyName}.{QueriesNamespace}");

            if (!t.Name.EndsWith(QuerySuffix))
                issues.Add($"ne se termine pas par {QuerySuffix}");

            details.Add($"{t.FullName} - {string.Join(", ", issues)}");
        });

        Assert.True(
            details.Count == 0,
            $"{Project} : Les Queries doivent respecter les conventions de nommage.{Environment.NewLine}{string.Join(Environment.NewLine, details)}");
    }

    [Fact]
    public void Application_Handlers_Should_Be_Sealed_And_Reside_In_Features_And_Ending_With_Handler()
    {
        const string HandlersNamespace = "Features";
        const string HandlerSuffix = "Handler";

        var details = new List<string>();

        var types = Types.InAssembly(ApplicationAssembly)
            .That().ImplementInterface(typeof(ICommandHandler<,>))
            .Or().ImplementInterface(typeof(IQueryHandler<,>))
            .GetTypes();

        var failingTypes = types
            .Where(t =>
                !t.IsSealed ||
                !t.FullName.StartsWith($"{ApplicationAssemblyName}.{HandlersNamespace}.")
                || !t.Name.EndsWith(HandlerSuffix))
            .ToList();

        failingTypes.ForEach(t =>
        {
            var issues = new List<string>();

            if (!t.IsSealed)
                issues.Add("n'est pas sealed");

            if (!t.FullName.StartsWith($"{ApplicationAssemblyName}.{HandlersNamespace}."))
                issues.Add($"n'est pas dans un namespace {ApplicationAssemblyName}.{HandlersNamespace}");

            if (!t.Name.EndsWith(HandlerSuffix))
                issues.Add($"ne se termine pas par {HandlerSuffix}");

            details.Add($"{t.FullName} - {string.Join(", ", issues)}");
        });

        Assert.True(
            details.Count == 0,
            $"{Project} : Les Handlers doivent respecter les conventions de nommage.{Environment.NewLine}{string.Join(Environment.NewLine, details)}");
    }

    [Fact]
    public void Application_Validators_Should_Be_Sealed_And_Reside_In_Features_And_Ending_With_Validator()
    {
        const string ValidatorsNamespace = "Features";
        const string CommandValidatorSuffix = "CommandValidator";
        const string QueryValidatorSuffix = "QueryValidator";

        var details = new List<string>();

        var types = Types.InAssembly(ApplicationAssembly)
            .That().Inherit(typeof(AbstractValidator<>))
            .GetTypes();

        var failingTypes = types
            .Where(t =>
                !t.IsSealed ||
                !t.FullName.StartsWith($"{ApplicationAssemblyName}.{ValidatorsNamespace}.") ||
                (!t.Name.EndsWith(CommandValidatorSuffix) && !t.Name.EndsWith(QueryValidatorSuffix)))
            .ToList();

        failingTypes.ForEach(t =>
        {
            var issues = new List<string>();

            if (!t.IsSealed)
                issues.Add("n'est pas sealed");

            if (!t.FullName.StartsWith($"{ApplicationAssemblyName}.{ValidatorsNamespace}."))
                issues.Add($"n'est pas dans un namespace {ApplicationAssemblyName}.{ValidatorsNamespace}");

            if (!t.Name.EndsWith(CommandValidatorSuffix) && !t.Name.EndsWith(QueryValidatorSuffix))
                issues.Add($"ne se termine pas par {CommandValidatorSuffix} ou {QueryValidatorSuffix}");

            details.Add($"{t.FullName} - {string.Join(", ", issues)}");
        });

        Assert.True(
            details.Count == 0,
            $"{Project} : Les Validators doivent respecter les conventions de nommage.{Environment.NewLine}{string.Join(Environment.NewLine, details)}");
    }

    [Fact]
    public void Application_Custom_Repositories_Should_Be_An_Interface_And_Reside_In_Interfaces_Persistence_And_Inherit_IRepository_And_IReadRepository_And_Starting_With_I_And_Ending_With_Repository()
    {
        const string CustomRepositoriesNamespace = "Interfaces.Persistence";
        const string CustomRepositoryPrefix = "I";
        const string CustomRepositorySuffix = "Repository";

        var details = new List<string>();

        var types = Types.InAssembly(ApplicationAssembly)
            .That().Inherit(typeof(IRepository<>))
            .Or().Inherit(typeof(IReadRepository<>))
            .Or().ResideInNamespace($"{ApplicationAssemblyName}.{CustomRepositoriesNamespace}")
            .Or().HaveNameEndingWith(CustomRepositorySuffix)
            .GetTypes();

        var failingTypes = types
            .Where(t =>
                !t.IsInterface ||
                !t.FullName.StartsWith($"{ApplicationAssemblyName}.{CustomRepositoriesNamespace}.") ||
                t.GetInterface(typeof(IRepository<>).Name) is null ||
                t.GetInterface(typeof(IReadRepository<>).Name) is null ||
                !t.Name.StartsWith(CustomRepositoryPrefix) ||
                !t.Name.EndsWith(CustomRepositorySuffix))
            .ToList();

        failingTypes.ForEach(t =>
        {
            var issues = new List<string>();

            if (!t.IsInterface)
                issues.Add("n'est pas une interface");

            if (!t.FullName.StartsWith($"{ApplicationAssemblyName}.{CustomRepositoriesNamespace}."))
                issues.Add($"n'est pas dans un namespace {ApplicationAssemblyName}.{CustomRepositoriesNamespace}");

            if (t.GetInterface(typeof(IRepository<>).Name) is null)
                issues.Add("n'hérite pas de IRepository<>");

            if (t.GetInterface(typeof(IReadRepository<>).Name) is null)
                issues.Add("n'hérite pas de IReadRepository<>");

            if (!t.Name.StartsWith(CustomRepositoryPrefix))
                issues.Add($"ne commence pas par {CustomRepositoryPrefix}");

            if (!t.Name.EndsWith(CustomRepositorySuffix))
                issues.Add($"ne se termine pas par {CustomRepositorySuffix}");

            details.Add($"{t.FullName} - {string.Join(", ", issues)}");
        });

        Assert.True(
            details.Count == 0,
            $"{Project} : Les Custom Repositories doivent respecter les conventions de nommage.{Environment.NewLine}{string.Join(Environment.NewLine, details)}");
    }
}