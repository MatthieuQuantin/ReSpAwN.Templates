using System.Diagnostics.CodeAnalysis;

namespace ModuleName.Presentation.Blazor;

[ExcludeFromCodeCoverage]
public static class NavRoutes
{
    //TODO : identifier les routes qui ne doivent pas être publiques mais internes
    public const string Counter = "/module-name/counter";
    public const string Weather = "/module-name/weather";
}