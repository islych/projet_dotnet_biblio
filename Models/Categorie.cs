using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliothequeWeb.Models;

/// <summary>
/// Classe représentant une catégorie de livres
/// </summary>
[Table("Categories")]
public class Categorie
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Le nom de la catégorie est obligatoire")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
    [Display(Name = "Nom")]
    public string Nom { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères")]
    [Display(Name = "Description")]
    [DataType(DataType.MultilineText)]
    public string? Description { get; set; }

    // Navigation property
    public virtual ICollection<Livre> Livres { get; set; } = new List<Livre>();
}

