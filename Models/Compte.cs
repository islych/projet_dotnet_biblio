using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliothequeWeb.Models;

/// <summary>
/// Classe abstraite représentant un compte utilisateur dans le système
/// Utilise l'héritage TPH (Table Per Hierarchy) avec Entity Framework Core
/// </summary>
[Table("Comptes")]
public abstract class Compte
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Le nom est obligatoire")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
    [Display(Name = "Nom")]
    public string Nom { get; set; } = string.Empty;

    [Required(ErrorMessage = "L'email est obligatoire")]
    [EmailAddress(ErrorMessage = "Format d'email invalide")]
    [StringLength(255, ErrorMessage = "L'email ne peut pas dépasser 255 caractères")]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le mot de passe est obligatoire")]
    [StringLength(255, MinimumLength = 6, ErrorMessage = "Le mot de passe doit contenir entre 6 et 255 caractères")]
    [Display(Name = "Mot de passe")]
    public string MotDePasse { get; set; } = string.Empty;

    [Display(Name = "Date d'inscription")]
    [DataType(DataType.DateTime)]
    public DateTime DateInscription { get; set; } = DateTime.Now;

    // Discriminator pour TPH
    [Required]
    [StringLength(50)]
    public string Discriminator { get; set; } = string.Empty;

    /// <summary>
    /// Méthode abstraite pour la connexion
    /// </summary>
    public abstract bool Login(string email, string motDePasse);

    /// <summary>
    /// Méthode pour la déconnexion
    /// </summary>
    public virtual void Logout()
    {
        // Logique de déconnexion
    }
}

