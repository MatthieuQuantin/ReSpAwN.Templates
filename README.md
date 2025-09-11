##### Compilation :
```bash
dotnet pack -c Release
```

##### Installation / Desinstallation locale :
```bash
dotnet new install .\bin\Release\ReSpAwN.Templates.1.0.0-alpha.nupkg
dotnet new uninstall ReSpAwN.Templates
```
##### Vidage du cache des templates :
```bash
devenv /updateConfiguration
```

##### Déploiement sur nuget.org :
```bash
dotnet nuget push .\bin\Release\ReSpAwN.Templates.1.0.0-alpha.nupkg --api-key [API_KEY_HERE] --source https://api.nuget.org/v3/index.json --skip-duplicate
```