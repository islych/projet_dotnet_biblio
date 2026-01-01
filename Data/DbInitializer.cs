using BibliothequeWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliothequeWeb.Data;

/// <summary>
/// Classe pour initialiser la base de données avec des données de test
/// </summary>
public static class DbInitializer
{
    public static async Task InitializeAsync(ApplicationDbContext context)
    {
        // S'assurer que la base de données est créée
        await context.Database.EnsureCreatedAsync();

        // Vérifier si des données existent déjà
        if (await context.Comptes.AnyAsync())
        {
            return; // La base de données a déjà été initialisée
        }

        // Créer un administrateur par défaut
        var admin = new Admin
        {
            Nom = "Administrateur",
            Email = "admin@bibliotheque.com",
            MotDePasse = "admin123", // En production, utiliser un hash
            DateInscription = DateTime.Now,
            Discriminator = "Admin"
        };
        context.Comptes.Add(admin);

        // Créer des catégories
        var categories = new[]
        {
            new Categorie { Nom = "Roman", Description = "Romans et fictions" },
            new Categorie { Nom = "Science", Description = "Livres scientifiques" },
            new Categorie { Nom = "Histoire", Description = "Livres d'histoire" },
            new Categorie { Nom = "Informatique", Description = "Livres sur l'informatique" }
        };
        context.Categories.AddRange(categories);
        await context.SaveChangesAsync();

        // Créer des livres
        var livres = new[]
        {
            new Livre
            {
                Titre = "Introduction à ASP.NET Core",
                Auteur = "John Doe",
                Description = "Un guide complet pour apprendre ASP.NET Core",
                DatePublication = new DateTime(2023, 1, 15),
                CategorieId = categories[3].Id
            },
            new Livre
            {
                Titre = "Les Misérables",
                Auteur = "Victor Hugo",
                Description = "Un classique de la littérature française",
                DatePublication = new DateTime(1862, 1, 1),
                CategorieId = categories[0].Id
            },
            new Livre
            {
                Titre = "Histoire de France",
                Auteur = "Pierre Dupont",
                Description = "Une histoire complète de la France",
                DatePublication = new DateTime(2020, 5, 10),
                CategorieId = categories[2].Id
            }
        };
        context.Livres.AddRange(livres);
        await context.SaveChangesAsync();
    }
}

