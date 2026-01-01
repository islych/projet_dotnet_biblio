# Bibliothèque Web - Application ASP.NET Core MVC

Application web complète de gestion de bibliothèque développée avec ASP.NET Core MVC (.NET 8), Razor Pages, Entity Framework Core et SQL Server.

## 📋 Fonctionnalités

### Authentification
- Connexion/Déconnexion par sessions
- Inscription utilisateur
- Gestion des rôles (Admin/Utilisateur)

### Gestion des Livres
- Catalogue de livres avec recherche et filtrage par catégorie
- Détails d'un livre
- Téléchargement de fichiers PDF
- Upload de fichiers PDF et images de couverture

### Favoris
- Ajout/Suppression de livres en favoris
- Liste des favoris par utilisateur

### Téléchargements
- Historique des téléchargements
- Suivi des téléchargements avec adresse IP

### Administration (Admin uniquement)
- Dashboard avec statistiques
- CRUD complet pour les livres
- CRUD complet pour les catégories
- Liste des utilisateurs

## 🏗️ Architecture

### Modèles
- **Compte** (classe abstraite) - Héritage TPH (Table Per Hierarchy)
  - **Utilisateur** : Utilisateur standard avec rôle
  - **Admin** : Administrateur avec droits de gestion
- **Livre** : Livres de la bibliothèque
- **Categorie** : Catégories de livres
- **Favori** : Relation Utilisateur-Livre
- **Telechargement** : Historique des téléchargements

### Pattern Repository + Service
- Séparation claire des responsabilités
- Repositories pour l'accès aux données
- Services pour la logique métier

### Base de données
- SQL Server avec Entity Framework Core (Code First)
- Migrations automatiques
- Relations configurées (OneToMany, ManyToOne)
- Contraintes et clés étrangères

## 🚀 Installation et Lancement

### Prérequis
- .NET 8 SDK
- SQL Server (LocalDB ou SQL Server Express)
- Visual Studio 2022 ou VS Code

### Étapes d'installation

1. **Cloner ou télécharger le projet**

2. **Configurer la chaîne de connexion**
   
   Modifiez `appsettings.json` avec votre chaîne de connexion SQL Server :
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BibliothequeDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
     }
   }
   ```

3. **Restaurer les packages NuGet**
   ```bash
   dotnet restore
   ```

4. **Créer la base de données**
   
   La base de données sera créée automatiquement au premier lancement grâce à `DbInitializer`.

5. **Lancer l'application**
   ```bash
   dotnet run
   ```
   
   Ou depuis Visual Studio : Appuyez sur F5

6. **Accéder à l'application**
   
   Ouvrez votre navigateur à l'adresse : `https://localhost:5001` ou `http://localhost:5000`

### Compte administrateur par défaut
- **Email** : `admin@bibliotheque.com`
- **Mot de passe** : `admin123`

## 📁 Structure du Projet

```
BibliothequeWeb/
├── Controllers/          # Controllers MVC
├── Data/                # DbContext et initialisation
├── Filters/             # Filtres d'autorisation
├── Attributes/          # Attributs personnalisés
├── Helpers/             # Helpers (SessionHelper)
├── Models/              # Modèles de données
├── Repositories/        # Pattern Repository
├── Services/            # Services métier
├── Views/               # Vues Razor
├── Pages/               # Razor Pages
├── wwwroot/             # Fichiers statiques (CSS, JS, images, uploads)
├── Program.cs           # Point d'entrée
└── appsettings.json     # Configuration
```

## 🔐 Sécurité

⚠️ **Note importante** : Cette application utilise une authentification simple par sessions pour la démonstration. En production, vous devriez :
- Utiliser un hashage de mot de passe (BCrypt, Argon2, etc.)
- Implémenter une authentification plus robuste (ASP.NET Core Identity)
- Ajouter des validations supplémentaires
- Implémenter CSRF protection (déjà présent avec ValidateAntiForgeryToken)

## 📝 Migrations Entity Framework

Si vous souhaitez utiliser les migrations EF Core au lieu de `EnsureCreated` :

```bash
# Créer une migration
dotnet ef migrations add InitialCreate

# Appliquer les migrations
dotnet ef database update
```

## 🎨 Technologies Utilisées

- **ASP.NET Core 8.0** (MVC + Razor Pages)
- **Entity Framework Core 8.0** (Code First)
- **SQL Server**
- **Bootstrap 5** (via CDN/lib)
- **jQuery** (pour les interactions AJAX)

## 📦 Packages NuGet

- `Microsoft.EntityFrameworkCore.SqlServer` (8.0.0)
- `Microsoft.EntityFrameworkCore.Tools` (8.0.0)
- `Microsoft.EntityFrameworkCore.Design` (8.0.0)

## 🛠️ Développement

### Ajouter un nouveau livre (Admin)
1. Se connecter en tant qu'admin
2. Aller dans Administration > Gérer les Livres
3. Cliquer sur "Ajouter un nouveau livre"
4. Remplir le formulaire et uploader un PDF/image si nécessaire

### Ajouter une catégorie (Admin)
1. Se connecter en tant qu'admin
2. Aller dans Administration > Gérer les Catégories
3. Cliquer sur "Ajouter une nouvelle catégorie"

### Utilisateur standard
1. S'inscrire ou se connecter
2. Parcourir le catalogue
3. Ajouter des livres aux favoris
4. Télécharger des PDF

## 📄 Licence

Ce projet est fourni à des fins éducatives et de démonstration.

## 👨‍💻 Support

Pour toute question ou problème, veuillez créer une issue dans le dépôt du projet.

