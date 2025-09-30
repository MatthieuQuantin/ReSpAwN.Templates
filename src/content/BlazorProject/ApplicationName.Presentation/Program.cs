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
        #region Initialiser Serilog en mode "bootstrap" pour capturer les logs t�t dans le cycle de vie de l'application avant que la configuration compl�te ne soit charg�e.

        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder().WithDefaultDestructurers())
            .WriteTo.Console()
            .CreateBootstrapLogger();

        #endregion

        var builder = WebApplication.CreateBuilder(args);

        #region Configuration de Serilog

        // Configurer Serilog pour qu'il lise la configuration depuis appsettings.json et int�gre les services de l'application.
        builder.Host.UseSerilog((context, services, cfg) =>
            cfg.ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                    .WithDefaultDestructurers()
                    .WithDestructurers(new[] { new DbUpdateExceptionDestructurer() })));

        #endregion

        // Ajouter les services personnalis�s.
        builder.Services.AddApplicationName(builder.Configuration, builder.Environment);

        // Permet d'acc�der au HttpContext dans les services (utile aussi pour Serilog)
        builder.Services.AddHttpContextAccessor();

        var app = builder.Build();

        #region Configuration de Serilog dans le pipeline HTTP pour capturer les logs des requ�tes HTTP

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

            // HSTS est un m�canisme de s�curit� qui force les clients � utiliser uniquement HTTPS.
            // La valeur par d�faut de HSTS est de 30 jours. Vous pouvez vouloir changer cela pour les sc�narios de production, voir https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.MapApplicationName();

        app.Run();
    }
}