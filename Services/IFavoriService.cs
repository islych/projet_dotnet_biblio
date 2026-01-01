using BibliothequeWeb.Models;

namespace BibliothequeWeb.Services;

/// <summary>
/// Interface du service pour la gestion des favoris
/// </summary>
public interface IFavoriService
{
    Task<IEnumerable<Favori>> GetByUserIdAsync(int utilisateurId);
    Task<Favori?> GetByUserAndLivreAsync(int utilisateurId, int livreId);
    Task<Favori> AddAsync(int utilisateurId, int livreId);
    Task RemoveAsync(int favoriId);
    Task<bool> IsFavoriAsync(int utilisateurId, int livreId);
}

