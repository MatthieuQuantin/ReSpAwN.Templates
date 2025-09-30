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
    //        .That().HaveNameStartingWith(ApplicationAssemblyName, StringComparison.Ordinal)
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

        var namespacePrefix = ApplicationAssemblyName + "." + CommandsNamespace;

        var types = Types.InAssembly(ApplicationAssembly)
            .That().ImplementInterface(typeof(ICommand<>))
            .GetTypes();

        var failingTypes = types
            .Where(t =>
                !t.IsSealed ||
                !t.FullName.StartsWith($"{namespacePrefix}.", StringComparison.Ordinal) ||
                !t.Name.EndsWith(CommandSuffix, StringComparison.Ordinal))
            .ToList();

        failingTypes.ForEach(t =>
        {
            var issues = new List<string>();

            if (!t.IsSealed)
                issues.Add("n'est pas sealed");

            if (!t.FullName.StartsWith($"{namespacePrefix}.", StringComparison.Ordinal))
                issues.Add($"n'est pas dans un namespace {namespacePrefix}");

            if (!t.Name.EndsWith(CommandSuffix, StringComparison.Ordinal))
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

        var namespacePrefix = ApplicationAssemblyName + "." + QueriesNamespace;

        var types = Types.InAssembly(ApplicationAssembly)
            .That().ImplementInterface(typeof(IQuery<>))
            .GetTypes();

        var failingTypes = types
            .Where(t =>
                !t.IsSealed ||
                !t.FullName.StartsWith($"{namespacePrefix}.", StringComparison.Ordinal)
                || !t.Name.EndsWith(QuerySuffix, StringComparison.Ordinal))
            .ToList();

        failingTypes.ForEach(t =>
        {
            var issues = new List<string>();

            if (!t.IsSealed)
                issues.Add("n'est pas sealed");

            if (!t.FullName.StartsWith($"{namespacePrefix}.", StringComparison.Ordinal))
                issues.Add($"n'est pas dans un namespace {namespacePrefix}");

            if (!t.Name.EndsWith(QuerySuffix, StringComparison.Ordinal))
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

        var namespacePrefix = ApplicationAssemblyName + "." + HandlersNamespace;

        var types = Types.InAssembly(ApplicationAssembly)
            .That().ImplementInterface(typeof(ICommandHandler<,>))
            .Or().ImplementInterface(typeof(IQueryHandler<,>))
            .GetTypes();

        var failingTypes = types
            .Where(t =>
                !t.IsSealed ||
                !t.FullName.StartsWith($"{namespacePrefix}.", StringComparison.Ordinal)
                || !t.Name.EndsWith(HandlerSuffix, StringComparison.Ordinal))
            .ToList();

        failingTypes.ForEach(t =>
        {
            var issues = new List<string>();

            if (!t.IsSealed)
                issues.Add("n'est pas sealed");

            if (!t.FullName.StartsWith($"{namespacePrefix}.", StringComparison.Ordinal))
                issues.Add($"n'est pas dans un namespace {namespacePrefix}");

            if (!t.Name.EndsWith(HandlerSuffix, StringComparison.Ordinal))
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

        var namespacePrefix = ApplicationAssemblyName + "." + ValidatorsNamespace;

        var types = Types.InAssembly(ApplicationAssembly)
            .That().Inherit(typeof(AbstractValidator<>))
            .GetTypes();

        var failingTypes = types
            .Where(t =>
                !t.IsSealed ||
                !t.FullName.StartsWith($"{namespacePrefix}.", StringComparison.Ordinal) ||
                (!t.Name.EndsWith(CommandValidatorSuffix, StringComparison.Ordinal) && !t.Name.EndsWith(QueryValidatorSuffix, StringComparison.Ordinal)))
            .ToList();

        failingTypes.ForEach(t =>
        {
            var issues = new List<string>();

            if (!t.IsSealed)
                issues.Add("n'est pas sealed");

            if (!t.FullName.StartsWith($"{namespacePrefix}.", StringComparison.Ordinal))
                issues.Add($"n'est pas dans un namespace {namespacePrefix}");

            if (!t.Name.EndsWith(CommandValidatorSuffix, StringComparison.Ordinal) && !t.Name.EndsWith(QueryValidatorSuffix, StringComparison.Ordinal))
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

        var namespacePrefix = ApplicationAssemblyName + "." + CustomRepositoriesNamespace;

        //TODO : A sortir
        bool ImplementInterface(Type type, Type @interface)
        {
            var allInterfaces = type.GetInterfaces();
            // Gère le cas des interfaces non génériques
            if (allInterfaces.Contains(@interface))
                return true;
            // Gère le cas des interfaces génériques
            return allInterfaces.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == @interface);
        }

        var types = Types.InAssembly(ApplicationAssembly)
            .That().HaveNameStartingWith(namespacePrefix, StringComparison.Ordinal)
            .Or().ImplementInterface(typeof(IRepository<>))
            .Or().ImplementInterface(typeof(IReadRepository<>))
            .GetTypes();

        foreach (var t in types)
        {
            var issues = new List<string>();

            if (!t.IsInterface)
                issues.Add("n'est pas une interface");

            if (t.Namespace == null || !t.Namespace.StartsWith(namespacePrefix, StringComparison.Ordinal))
                issues.Add($"n'est pas dans un namespace {namespacePrefix}");

            if (!ImplementInterface(t, typeof(IRepository<>)) && !ImplementInterface(t, typeof(IReadRepository<>)))
                issues.Add("n'hérite pas de IRepository<> ni de IReadRepository<>");

            // Nom sans l'arité des génériques (évite le `1, `2, …)
            var idx = t.Name.IndexOf('`');
            var baseName = idx >= 0 ? t.Name.Substring(0, idx) : t.Name;

            if (!baseName.StartsWith(CustomRepositoryPrefix, StringComparison.Ordinal))
                issues.Add($"ne commence pas par {CustomRepositoryPrefix}");

            if (!baseName.EndsWith(CustomRepositorySuffix, StringComparison.Ordinal))
                issues.Add($"ne se termine pas par {CustomRepositorySuffix}");

            if (issues.Count > 0)
                details.Add($"{t.FullName} - {string.Join(", ", issues)}");
        }

        Assert.True(
            details.Count == 0,
            $"{Project} : Les Custom Repositories doivent respecter les conventions de nommage.{Environment.NewLine}{string.Join(Environment.NewLine, details)}");
    }
}