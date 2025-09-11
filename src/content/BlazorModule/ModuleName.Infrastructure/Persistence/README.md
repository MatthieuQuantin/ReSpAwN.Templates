> [!WARNING]
> Attention modifier les appsetings pour cibler la bonne base de données


### Installation/Mise à jour des outils Entity Framework Core
```bash
dotnet tool install --global dotnet-ef
```


### Initialisation de la migration sur une nouvelle base ou une base vide
```bash
# Générer la migration InitialCreate
dotnet ef migrations add InitialCreate --project ModuleName/ModuleName.Infrastructure --startup-project ApplicationName/ApplicationName.Presentation --context ModuleNameDbContext --output-dir Persistence/Migrations
# version courte
dotnet ef migrations add InitialCreate -p ModuleName/ModuleName.Infrastructure -s ApplicationName/ApplicationName.Presentation -c ModuleNameDbContext -o Persistence/Migrations

# Appliquer la migration InitialCreate à la base de données
dotnet ef database update --project ModuleName/ModuleName.Infrastructure --startup-project ApplicationName/ApplicationName.Presentation --context ModuleNameDbContext
# version courte
dotnet ef database update -p ModuleName/ModuleName.Infrastructure -s ApplicationName/ApplicationName.Presentation -c ModuleNameDbContext
```


### Intialisation de la migration sur une base existante
```bash
# Générer les modèles à partir de la base de données existante
dotnet ef dbcontext scaffold Name=ConnectionStrings:ModuleNameConnection Microsoft.EntityFrameworkCore.SqlServer --startup-project ApplicationName/ApplicationName.Presentation --project ModuleName/ModuleName.Domain --output-dir Models --context ModuleNameDbContext --use-database-names
# version courte
dotnet ef dbcontext scaffold Name=ConnectionStrings:ModuleNameConnection Microsoft.EntityFrameworkCore.SqlServer -s ApplicationName/ApplicationName.Presentation -p ModuleName/ModuleName.Domain -o Models -c ModuleNameDbContext --use-database-names
```
Il faut ensuite déplacer le DbContext dans ModuleName.Infrastructure.Persistence et modifier le namespace.
Idéalement il faudrait déplacer les configurations présentes dans le DbContext dans des classes séparées dans le dossier Configurations.

```bash
# Générer la migration Baseline
dotnet ef migrations add Baseline --project ModuleName/ModuleName.Infrastructure --startup-project ApplicationName/ApplicationName.Presentation --context ModuleNameDbContext --output-dir Persistence/Migrations
# version courte
dotnet ef migrations add Baseline -p ModuleName/ModuleName.Infrastructure -s ApplicationName/ApplicationName.Presentation -c ModuleNameDbContext -o Persistence/Migrations
```
Il faut ensuite vider les méthodes Up() et Down() de la migration Baseline et supprimer le fichier .designer.cs associé.

```bash
# Appliquer la migration Baseline à la base de données
dotnet ef database update --project ModuleName/ModuleName.Infrastructure --startup-project ApplicationName/ApplicationName.Presentation --context ModuleNameDbContext
# version courte
dotnet ef database update -p ModuleName/ModuleName.Infrastructure -s ApplicationName/ApplicationName.Presentation -c ModuleNameDbContext
```

### Ajouter une nouvelle migration
```bash
dotnet ef migrations add MigrationName --project ModuleName/ModuleName.Infrastructure --startup-project ApplicationName/ApplicationName.Presentation --context ModuleNameDbContext --output-dir Persistence/Migrations
# version courte
dotnet ef migrations add MigrationName -p ModuleName/ModuleName.Infrastructure -s ApplicationName/ApplicationName.Presentation -c ModuleNameDbContext -o Persistence/Migrations
```

### Appliquer une migration (mise à jour de la BDD)
```bash
dotnet ef database update --project ModuleName/ModuleName.Infrastructure --startup-project ApplicationName/ApplicationName.Presentation --context ModuleNameDbContext
# version courte
dotnet ef database update -p ModuleName/ModuleName.Infrastructure -s ApplicationName/ApplicationName.Presentation -c ModuleNameDbContext
```

### Aller à une migration spécifique
```bash
dotnet ef database update MigrationName --project ModuleName/ModuleName.Infrastructure --startup-project ApplicationName/ApplicationName.Presentation --context ModuleNameDbContext
# version courte
dotnet ef database update MigrationName -p ModuleName/ModuleName.Infrastructure -s ApplicationName/ApplicationName.Presentation -c ModuleNameDbContext
```

Remplacer `MigrationName` par le nom de la migration cible ou `0` pour revenir à l'état initial (sans aucune migration appliquée).