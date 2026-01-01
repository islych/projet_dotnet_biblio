using System.ComponentModel.DataAnnotations.Schema;

namespace BibliothequeWeb.Models;

/// <summary>
/// Classe Admin héritant de Compte
/// Représente un administrateur avec des droits de gestion
/// </summary>
[Table("Comptes")]
public class Admin : Compte
{
    // Navigation properties pour la gestion
    // Les relations sont gérées via les services

    public override bool Login(string email, string motDePasse)
    {
        // Vérification simple (en production, utiliser un hash)
        return Email == email && MotDePasse == motDePasse;
    }

    /// <summary>
    /// Méthode pour gérer les livres
    /// </summary>
    public void GererLivres()
    {
        // Logique de gestion des livres
    }

    /// <summary>
    /// Méthode pour gérer les catégories
    /// </summary>
    public void GererCategories()
    {
        // Logique de gestion des catégories
    }

    /// <summary>
    /// Méthode pour gérer les utilisateurs
    /// </summary>
    public void GererUtilisateurs()
    {
        // Logique de gestion des utilisateurs
    }
}

