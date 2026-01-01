using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliothequeWeb.Models;

/// <summary>
/// Classe représentant un téléchargement (relation entre Utilisateur et Livre)
/// </summary>
[Table("Telechargements")]
public class Telechargement
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Utilisateur")]
    public int UtilisateurId { get; set; }

    [Required]
    [Display(Name = "Livre")]
    public int LivreId { get; set; }

    [Display(Name = "Date de téléchargement")]
    [DataType(DataType.DateTime)]
    public DateTime DateTelechargement { get; set; } = DateTime.Now;

    [StringLength(255)]
    [Display(Name = "Adresse IP")]
    public string? AdresseIp { get; set; }

    // Navigation properties
    [ForeignKey("UtilisateurId")]
    public virtual Utilisateur Utilisateur { get; set; } = null!;

    [ForeignKey("LivreId")]
    public virtual Livre Livre { get; set; } = null!;
}

