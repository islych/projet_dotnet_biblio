using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliothequeWeb.Models;

/// <summary>
/// Classe Utilisateur héritant de Compte
/// Représente un utilisateur standard du système
/// </summary>
[Table("Comptes")]
public class Utilisateur : Compte
{
    [StringLength(50, ErrorMessage = "Le rôle ne peut pas dépasser 50 caractères")]
    [Display(Name = "Rôle")]
    public string Role { get; set; } = "Utilisateur";

    // Navigation properties
    public virtual ICollection<Favori> Favoris { get; set; } = new List<Favori>();
    public virtual ICollection<Telechargement> Telechargements { get; set; } = new List<Telechargement>();

    public override bool Login(string email, string motDePasse)
    {
        // Vérification simple (en production, utiliser un hash)
        return Email == email && MotDePasse == motDePasse;
    }
}

