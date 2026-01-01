# Migrations Entity Framework Core

Ce document explique comment utiliser les migrations EF Core au lieu de `EnsureCreated()`.

## Utiliser les Migrations

### 1. Installer les outils EF Core (si pas déjà fait)

```bash
dotnet tool install --global dotnet-ef
```

### 2. Créer une migration initiale

```bash
dotnet ef migrations add InitialCreate
```

Cela créera un dossier `Migrations/` avec les fichiers de migration.

### 3. Appliquer les migrations à la base de données

```bash
dotnet ef database update
```

### 4. Modifier Program.cs

Remplacez dans `Program.cs` :

```csharp
// Initialisation de la base de données au démarrage
await using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await BibliothequeWeb.Data.DbInitializer.InitializeAsync(context);
}
```

Par :

```csharp
// Les migrations sont appliquées manuellement avec dotnet ef database update
// Initialisation des données de base
await using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await BibliothequeWeb.Data.DbInitializer.InitializeAsync(context);
}
```

Et dans `ApplicationDbContext`, vous pouvez retirer `EnsureCreated()` si vous utilisez les migrations.

## Commandes utiles

- **Créer une nouvelle migration** : `dotnet ef migrations add NomMigration`
- **Appliquer les migrations** : `dotnet ef database update`
- **Supprimer la dernière migration** : `dotnet ef migrations remove`
- **Voir le script SQL** : `dotnet ef migrations script`

## Note

Par défaut, le projet utilise `EnsureCreated()` qui crée automatiquement la base de données au démarrage. C'est pratique pour le développement mais les migrations sont recommandées pour la production.

