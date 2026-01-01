using BibliothequeWeb.Models;

namespace BibliothequeWeb.Services;

/// <summary>
/// Interface du service pour la gestion des livres
/// </summary>
public interface ILivreService
{
    Task<Livre?> GetByIdAsync(int id);
    Task<IEnumerable<Livre>> GetAllAsync();
    Task<IEnumerable<Livre>> GetByCategorieIdAsync(int categorieId);
    Task<IEnumerable<Livre>> SearchAsync(string searchTerm);
    Task<Livre> CreateAsync(Livre livre);
    Task UpdateAsync(Livre livre);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}

