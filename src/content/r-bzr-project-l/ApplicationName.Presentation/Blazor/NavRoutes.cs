using System.Diagnostics.CodeAnalysis;

namespace ApplicationName.Presentation.Blazor;

[ExcludeFromCodeCoverage]
public static class NavRoutes
{
    //TODO : identifier les routes qui ne doivent pas être publiques mais internes
    public const string Counter = "/counter";
    public const string Error = "/error";
    public const string Home = "/";
    public const string Weather = "/weather";
}