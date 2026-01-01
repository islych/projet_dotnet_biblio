using BibliothequeWeb.Models;

namespace BibliothequeWeb.Repositories;

/// <summary>
/// Interface du repository pour la gestion des favoris
/// </summary>
public interface IFavoriRepository
{
    Task<Favori?> GetByIdAsync(int id);
    Task<Favori?> GetByUserAndLivreAsync(int utilisateurId, int livreId);
    Task<IEnumerable<Favori>> GetByUserIdAsync(int utilisateurId);
    Task<Favori> AddAsync(Favori favori);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int utilisateurId, int livreId);
}

