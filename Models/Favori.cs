using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliothequeWeb.Models;

/// <summary>
/// Classe représentant un favori (relation entre Utilisateur et Livre)
/// </summary>
[Table("Favoris")]
public class Favori
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Utilisateur")]
    public int UtilisateurId { get; set; }

    [Required]
    [Display(Name = "Livre")]
    public int LivreId { get; set; }

    [Display(Name = "Date d'ajout")]
    [DataType(DataType.DateTime)]
    public DateTime DateAjout { get; set; } = DateTime.Now;

    // Navigation properties
    [ForeignKey("UtilisateurId")]
    public virtual Utilisateur Utilisateur { get; set; } = null!;

    [ForeignKey("LivreId")]
    public virtual Livre Livre { get; set; } = null!;
}

