using ApplicationName.Presentation.Blazor;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;

namespace ApplicationName.Presentation;

public static class Program
{
    public static void Main(string[] args)
    {
        #region Initialiser Serilog en mode "bootstrap" pour capturer les logs tôt dans le cycle de vie de l'application avant que la configuration complète ne soit chargée.

        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder().WithDefaultDestructurers())
            .WriteTo.Console()
            .CreateBootstrapLogger();

        #endregion

        var builder = WebApplication.CreateBuilder(args);

        #region Configuration de Serilog

        // Configurer Serilog pour qu'il lise la configuration depuis appsettings.json et intègre les services de l'application.
        builder.Host.UseSerilog((context, services, cfg) =>
            cfg.ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                    .WithDefaultDestructurers()
                    .WithDestructurers(new[] { new DbUpdateExceptionDestructurer() })));

        #endregion

        // Ajouter les services personnalisés.
        builder.Services.AddApplicationName(builder.Configuration, builder.Environment);

        // Permet d'accéder au HttpContext dans les services (utile aussi pour Serilog)
        builder.Services.AddHttpContextAccessor();

        var app = builder.Build();

        #region Configuration de Serilog dans le pipeline HTTP pour capturer les logs des requêtes HTTP

        app.UseSerilogRequestLogging(options =>
        {
            options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            {
                diagnosticContext.Set("Host", httpContext.Request.Host.Value);
                diagnosticContext.Set("Scheme", httpContext.Request.Scheme);
            };
        });

        #endregion

        // Configurer le pipeline HTTP.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler(NavRoutes.Error);

            // HSTS est un mécanisme de sécurité qui force les clients à utiliser uniquement HTTPS.
            // La valeur par défaut de HSTS est de 30 jours. Vous pouvez vouloir changer cela pour les scénarios de production, voir https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.MapApplicationName();

        app.Run();
    }
}