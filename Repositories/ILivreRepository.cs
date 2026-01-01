using BibliothequeWeb.Models;

namespace BibliothequeWeb.Repositories;

/// <summary>
/// Interface du repository pour la gestion des livres
/// </summary>
public interface ILivreRepository
{
    Task<Livre?> GetByIdAsync(int id);
    Task<IEnumerable<Livre>> GetAllAsync();
    Task<IEnumerable<Livre>> GetByCategorieIdAsync(int categorieId);
    Task<IEnumerable<Livre>> SearchAsync(string searchTerm);
    Task<Livre> AddAsync(Livre livre);
    Task UpdateAsync(Livre livre);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}

