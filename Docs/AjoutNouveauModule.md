Ce document explique comment ajouter un module à une application Blazor créée à l'aide de ce Package.
---
1️⃣ Création du module
---
1. Dans le dossier **``Modules``** de la solution dans Visual Studio, créez un **nouveau dossier de solution** portant le nom du module.

   ➡️ Ex. : ``Meteo``

2. À l'intérieur de ce dossier, ajoutez un **nouveau projet de type "Module Blazor"** et nommez-le : ``ModuleMeteo``

   💡 Assurez-vous de bien sélectionner votre emplacement : le module doit être créé dans le dossier ``src``.

3. La création du module génère automatiquement plusieurs projets dans un dossier ``src/ModuleMeteo`` :
    * ``ModuleMeteo.Application``
    * ``ModuleMeteo.Domain``
    * ``ModuleMeteo.Domain.Contracts``
    * ``ModuleMeteo.Infrastructure``
    * ``ModuleMeteo.Presentation``
    * ``ModuleMeteo.Tests``


    ℹ️ Si vous avez oublié de configurer correctement le chemin de création du module, ou si vos sources ne sont pas placées au bon endroit, vous pouvez suivre les étapes suivantes :
        
        1. Fermez Visual Studio.
        2. Ouvrez le dossier contenant la solution principale. Vous devriez y retrouver un dossier nommé ``src``.
        3. Si un dossier ``ModuleMeteo`` a été créé à un autre endroit que dans le dossier ``src``, **déplacez-le** dans ``src``, puis **supprimez** le dossier caché ``.vs``.
        4. Rouvrez la solution dans VisualStudio :
            - Si les projets du module ne sont plus détectés, ``supprimez-les de la solution``.
            - Faites un clic droit sur le dossier de solution ``Meteo`` → **Ajouter > Projet existant...**
            - Sélectionnez tous les fichiers ``.csproj`` correspondants.
        5. Une fois les projets correctement ajoutés et placés, rebuild la solution.

***
2️⃣ Intégration du module dans l'application principale
---
1. Sur les dépendances du projet ``[MyApp].Presentation``, faites **Clic droit > Ajouter une référence de projet...** et **ajoutez une référence** vers le projet ``ModuleMeteo.Presentation``.
2. Dans le fichier ``DependencyInjection.cs`` du projet ``[MyApp].Presentation``, ajoutez les lignes suivantes :
    ````csharp
    .AddModuleMeteo(configuration, environment) // Après services.AddModuleAuthentification(configuration, environment)
        
    .MapModuleMeteo() // Après builder.MapModuleAuthentification()
    
    .MapModuleMeteoComponents() // Après builder.MapModuleAuthentificationComponents()
    ````

***
3️⃣ Configuration du module
---
### 3.1 Références et Shared Kernel
#### a. Configuration du contexte
Dans ``ModuleMeteo.Infrastructure`` :

1. Rendez-vous dans ``Persistence > Configurations`` et **mettez en commentaire** le contenu des deux fichiers de configuration existants afin d'éviter tout conflit lors de la création du schéma de base de données.
2. Dans les **dépendances du projet**, via le **Package Manager** :
    * Supprimez la dépendance à ``SqlServer``.
    * Installez le package **PostgreSQL** par ``Npgsql``.
3. Dans le fichier ``ModuleMeteoDbContext``, remplacez ``optionsBuilder.UseSqlServer(...)`` par ``optionsBuilder.UseNpgsql(...)``.


    ℹ️ Les étapes 2 et 3 ne concernent votre application que si votre base de données utilise PostGreSQL.

#### b. Fichier ``Constants.cs``
Modifiez :
* ``CONNECTION_STRING_NAME`` → utilisez le nom du paramètre de connexion défini dans le fichier ``appsettings.json`` du projet ``[MyApp].Presentation``.
* ``DBCONTEXT_SCHEMA_NAME`` → définissez le nom du schéma du module (ex. : ``meteo``).

#### c. Fichier ``README.md``
Mettez à jour les **chemins** indiqués pour correspondre à la structure réelle de votre solution.
Cela facilitera la création des migrations et la génération de nouvelles tables.

    ℹ️ Exemple

        La commande
            
            dotnet ef migrations add InitialCreate -p ModuleMeteo/ModuleMeteo.Infrastructure -s ApplicationName/ApplicationName.Presentation -c ModuleMeteoDbContext -o Persistence/Migrations

        devient
            
            dotnet ef migrations add InitialCreate -p src/ModuleMeteo/ModuleMeteo.Infrastructure -s src/[MyApp]/[MyApp].Presentation -c ModuleMeteoDbContext -o Persistence/Migrations

#### d. Références
Recherchez les références commençant par ``ApplicationName`` et remplacez par le nom de votre application pour éviter des erreurs au build de la solution.

***
4️⃣ Création et exécution des migrations
---
1. **Rebuild** la solution. Dans la fenêtre **PowerShell Développeur**, exécutez la commande :

   ``dotnet ef migrations add InitialCreate -p src/ModuleMeteo/ModuleMeteo.Infrastructure -s src/[MyApp]/[MyApp].Presentation -c ModuleMeteoDbContext -o Persistence/Migrations``

2. Ouvrez le fichier de migration généré et ajoutez dans la méthode ``Up`` :
    ````csharp
    migrationBuilder.Sql($"CREATE SCHEMA IF NOT EXISTS {Constants.DBCONTEXT_SCHEMA_NAME}");
    ````
3. **Rebuild** la solution.
4. Exécutez ensuite :

   ``dotnet ef database update -p src/ModuleMeteo/ModuleMeteo.Infrastructure -s src/[MyApp]/[MyApp].Presentation -c ModuleMeteoDbContext``

***
5️⃣ Vérifications et finalisation
---
* Si des erreurs de compilation apparaissent, vérifiez que vos références utilisent bien le nom de votre application et non le placeholder ``ApplicationName``.
* Une fois la compilation et la migration effectuées avec succès, votre module est **prêt à être utilisé**.