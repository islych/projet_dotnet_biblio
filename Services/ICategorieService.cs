using BibliothequeWeb.Models;

namespace BibliothequeWeb.Services;

/// <summary>
/// Interface du service pour la gestion des catégories
/// </summary>
public interface ICategorieService
{
    Task<Categorie?> GetByIdAsync(int id);
    Task<IEnumerable<Categorie>> GetAllAsync();
    Task<Categorie> CreateAsync(Categorie categorie);
    Task UpdateAsync(Categorie categorie);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}

