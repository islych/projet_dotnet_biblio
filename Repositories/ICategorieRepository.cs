using BibliothequeWeb.Models;

namespace BibliothequeWeb.Repositories;

/// <summary>
/// Interface du repository pour la gestion des catégories
/// </summary>
public interface ICategorieRepository
{
    Task<Categorie?> GetByIdAsync(int id);
    Task<IEnumerable<Categorie>> GetAllAsync();
    Task<Categorie> AddAsync(Categorie categorie);
    Task UpdateAsync(Categorie categorie);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}

