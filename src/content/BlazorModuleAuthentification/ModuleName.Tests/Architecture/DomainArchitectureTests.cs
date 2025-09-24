using Ardalis.SharedKernel;
using Ardalis.Specification;
using NetArchTest.Rules;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ModuleName.Tests.Architecture;

[ExcludeFromCodeCoverage]
public class DomainArchitectureTests : BaseArchitectureTests
{
    [Fact]
    public void Domain_Should_Only_Have_Dependencies_On_Allowed_Dependencies()
    {
        // Définition des namespaces/autres dépendances autorisées
        var allowedDependencies = new[] {
            "System",
            "Vogen",
            "Ardalis.GuardClauses",
            "Ardalis.Result",
            "Ardalis.SharedKernel",
            "Ardalis.Specification",
            "Microsoft.AspNetCore.Identity",
            DomainAssemblyName
        };

        // Récupère tous les types de l'assembly Domain qui doivent respecter les contraintes
        var result = Types.InAssembly(DomainAssembly)
            .That().HaveNameStartingWith(DomainAssemblyName, StringComparison.Ordinal)
            .Should().OnlyHaveDependenciesOn(allowedDependencies)
            .GetResult();

        // Si le test échoue, on inspecte les détails
        if (!result.IsSuccessful)
        {
            var details = new List<string>();

            foreach (var type in result.FailingTypes)
            {
                // Vérifie que le type parent (héritage) est autorisé
                var baseType = type.BaseType;
                var typeWithWrongParentType = baseType != null
                    && baseType != typeof(object)
                    && baseType.Namespace != null
                    && !allowedDependencies.Any(allowed => baseType.Namespace.StartsWith(allowed, StringComparison.Ordinal));

                // Récupère tous les types référencés dans les membres (propriétés, champs, méthodes, événements)
                var memberReferencedTypes = type
                    .GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                    .SelectMany(m =>
                    {
                        var memberType = m switch
                        {
                            PropertyInfo pi => pi.PropertyType,
                            FieldInfo fi => fi.FieldType,
                            MethodInfo mi => mi.ReturnType,
                            EventInfo ei => ei.EventHandlerType,
                            _ => null
                        };
                        // Extraction récursive des types génériques/imbriqués
                        return memberType != null ? ExtractAllTypes(memberType) : Enumerable.Empty<Type>();
                    })
                    .Where(t => t.Namespace != null)
                    .Distinct()
                    .Where(t => !allowedDependencies.Any(allowed => t.Namespace!.StartsWith(allowed, StringComparison.Ordinal)))
                    .Select(t => t.FullName)
                    .ToList();

                // Vérifie les types d'attributs utilisés (sur le type lui-même)
                var attributeTypes = type.GetCustomAttributesData()
                    .Select(attr => attr.AttributeType);

                // Vérifie les attributs appliqués aux membres (propriétés, méthodes, etc.)
                var memberAttributeTypes = type
                    .GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                    .SelectMany(m => m.GetCustomAttributesData().Select(attr => attr.AttributeType));

                // Liste des attributs dont le namespace n’est pas autorisé
                var allAttributeTypes = attributeTypes.Concat(memberAttributeTypes)
                    .Where(t => t?.Namespace != null)
                    .Distinct()
                    .Where(t => !allowedDependencies.Any(allowed => t!.Namespace!.StartsWith(allowed, StringComparison.Ordinal)))
                    .Select(t => t!.FullName)
                    .ToList();

                // Ajoute un message pour héritage non autorisé
                if (typeWithWrongParentType)
                    details.Add($"{type.FullName} a un parent non autorisé : {baseType?.FullName}");

                // Ajoute un message pour types utilisés dans les membres non autorisés
                if (memberReferencedTypes.Any())
                    details.Add($"{type.FullName} référence des types non autorisés : {string.Join(", ", memberReferencedTypes)}");

                // Ajoute un message pour les attributs non autorisés
                if (allAttributeTypes.Any())
                    details.Add($"{type.FullName} utilise des attributs non autorisés : {string.Join(", ", allAttributeTypes)}");

                // Si aucune cause directe trouvée, on suspecte une dépendance indirecte
                if (!typeWithWrongParentType && !memberReferencedTypes.Any() && !allAttributeTypes.Any())
                    details.Add($"{type.FullName} (référence non trouvée, dépendance indirecte probable)");
            }

            // Affiche les erreurs détaillées
            Assert.True(result.IsSuccessful,
                $"{Project} : Le domaine ne doit dépendre que de lui-même ou des dépendances autorisées.{Environment.NewLine}{string.Join(Environment.NewLine, details)}");
        }

        // Double vérification en fin de test (fallback si pas déjà levé)
        Assert.True(result.IsSuccessful, $"{Project} : Le domaine ne doit dépendre que de lui-même ou des dépendances autorisées.{Environment.NewLine}{string.Join(Environment.NewLine, result.FailingTypeNames ?? [])}");

        // Méthode utilitaire : extraire tous les types imbriqués d’un type (ex: List<Infra.MyType>)
        static IEnumerable<Type> ExtractAllTypes(Type type)
        {
            yield return type;

            // Si c’est un type générique, on parcourt ses arguments
            if (type.IsGenericType)
            {
                foreach (var arg in type.GetGenericArguments())
                {
                    foreach (var inner in ExtractAllTypes(arg))
                        yield return inner;
                }
            }

            // Si c’est un tableau ou pointeur ou autre élément composé
            if (type.HasElementType)
            {
                foreach (var inner in ExtractAllTypes(type.GetElementType()!))
                    yield return inner;
            }
        }
    }

    [Fact]
    public void Domain_Classes_Should_Be_Sealed_Or_Abstract()
    {
        var result = Types.InAssembly(DomainAssembly)
            .That().AreClasses()
            .And().HaveNameStartingWith(DomainAssemblyName, StringComparison.Ordinal)
            .And().DoNotHaveNameEndingWith("Converter")// Exclut les Converter générés par Vogen
            .Should().BeSealed()
            .Or().BeAbstract()
            .GetResult();

        var details = result.FailingTypes
            ?.Select(t => $"{t.FullName} - est {(t.IsAbstract ? "abstract" : (t.IsSealed ? "sealed" : "ni sealed ni abstract"))}")
            ?? [];

        Assert.True(result.IsSuccessful, $"{Project} : Les classes du domaine doivent être scellées ou abstraites.{Environment.NewLine}{string.Join(Environment.NewLine, details)}");
    }

    //[Fact]
    //public void Domain_Events_Should_Be_Sealed_And_Reside_In_Aggregates_Events_And_Ending_With_Event()
    //{
    //    const string EventsNamespace = "Aggregate.Events";
    //    const string EventSuffix = "Event";

    //    var types = Types.InAssembly(DomainContractsAssembly)
    //        .That().Inherit(typeof(DomainEventBase))
    //        .GetTypes();

    //    var failingTypes = types
    //        .Where(t =>
    //            !t.IsSealed ||
    //            (!t.FullName.StartsWith(DomainContractsAssemblyName, StringComparison.Ordinal) || !t.FullName.Contains($"{EventsNamespace}.", StringComparison.Ordinal)) ||
    //            !t.Name.EndsWith(EventSuffix, StringComparison.Ordinal))
    //        .ToList();

    //    var details = new List<string>();

    //    failingTypes.ForEach(t =>
    //    {
    //        var issues = new List<string>();

    //        if (!t.IsSealed)
    //            issues.Add("n'est pas sealed");

    //        if (!t.FullName.StartsWith(DomainContractsAssemblyName, StringComparison.Ordinal) || !t.FullName.Contains($"{EventsNamespace}.", StringComparison.Ordinal))
    //            issues.Add($"n'est pas dans un namespace {DomainContractsAssemblyName}.*{EventsNamespace}");

    //        if (!t.Name.EndsWith(EventSuffix, StringComparison.Ordinal))
    //            issues.Add($"ne se termine pas par {EventSuffix}");

    //        details.Add($"{t.FullName} - {string.Join(", ", issues)}");
    //    });

    //    Assert.True(failingTypes.Count == 0, $"{Project} : Les DomainEvents doivent être sealed, dans un namespace valide et bien nommés.{Environment.NewLine}{string.Join(Environment.NewLine, details)}");
    //}

    [Fact]
    public void Domain_Specifications_Should_Be_Sealed_And_Reside_In_Aggregates_Specifications_And_Ending_With_Specification()
    {
        const string SpecificationsNamespace = "Aggregate.Specifications";
        const string SpecificationSuffix = "Specification";

        var types = Types.InAssembly(DomainAssembly)
            .That().Inherit(typeof(SingleResultSpecification<>))
            .Or().Inherit(typeof(Specification<>))
            .GetTypes();

        var failingTypes = types
            .Where(t =>
                !t.IsSealed ||
                (!t.FullName.StartsWith(DomainContractsAssemblyName, StringComparison.Ordinal) || !t.FullName.Contains($"{SpecificationsNamespace}.", StringComparison.Ordinal)) ||
                !t.Name.EndsWith(SpecificationSuffix, StringComparison.Ordinal))
            .ToList();

        var details = new List<string>();

        failingTypes.ForEach(t =>
        {
            var issues = new List<string>();

            if (!t.IsSealed)
                issues.Add("n'est pas sealed");

            if (!t.FullName.StartsWith(DomainContractsAssemblyName, StringComparison.Ordinal) || !t.FullName.Contains($"{SpecificationsNamespace}.", StringComparison.Ordinal))
                issues.Add($"n'est pas dans un namespace {DomainContractsAssemblyName}.*{SpecificationsNamespace}");

            if (!t.Name.EndsWith(SpecificationSuffix, StringComparison.Ordinal))
                issues.Add($"ne se termine pas par {SpecificationSuffix}");

            details.Add($"{t.FullName} - {string.Join(", ", issues)}");
        });

        Assert.True(
            failingTypes.Count == 0,
            $"{Project} : Les Specifications doivent être sealed, dans un namespace valide et bien nommés.{Environment.NewLine}{string.Join(Environment.NewLine, details)}");
    }
}