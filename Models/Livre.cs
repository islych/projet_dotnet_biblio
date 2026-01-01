using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliothequeWeb.Models;

/// <summary>
/// Classe représentant un livre dans la bibliothèque
/// </summary>
[Table("Livres")]
public class Livre
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Le titre est obligatoire")]
    [StringLength(200, ErrorMessage = "Le titre ne peut pas dépasser 200 caractères")]
    [Display(Name = "Titre")]
    public string Titre { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "L'auteur ne peut pas dépasser 100 caractères")]
    [Display(Name = "Auteur")]
    public string? Auteur { get; set; }

    [StringLength(1000, ErrorMessage = "La description ne peut pas dépasser 1000 caractères")]
    [Display(Name = "Description")]
    [DataType(DataType.MultilineText)]
    public string? Description { get; set; }

    [Display(Name = "Date de publication")]
    [DataType(DataType.Date)]
    public DateTime? DatePublication { get; set; }

    [StringLength(255, ErrorMessage = "Le chemin du fichier ne peut pas dépasser 255 caractères")]
    [Display(Name = "Fichier PDF")]
    public string? FichierPdf { get; set; }

    [StringLength(255, ErrorMessage = "L'image ne peut pas dépasser 255 caractères")]
    [Display(Name = "Image de couverture")]
    public string? ImageCouverture { get; set; }

    [Display(Name = "Catégorie")]
    public int? CategorieId { get; set; }

    // Navigation properties
    [ForeignKey("CategorieId")]
    public virtual Categorie? Categorie { get; set; }

    public virtual ICollection<Favori> Favoris { get; set; } = new List<Favori>();
    public virtual ICollection<Telechargement> Telechargements { get; set; } = new List<Telechargement>();
}

