using BibliothequeWeb.Models;
using BibliothequeWeb.Repositories;

namespace BibliothequeWeb.Services;

/// <summary>
/// Service pour la gestion des favoris
/// </summary>
public class FavoriService : IFavoriService
{
    private readonly IFavoriRepository _favoriRepository;

    public FavoriService(IFavoriRepository favoriRepository)
    {
        _favoriRepository = favoriRepository;
    }

    public async Task<IEnumerable<Favori>> GetByUserIdAsync(int utilisateurId)
    {
        return await _favoriRepository.GetByUserIdAsync(utilisateurId);
    }

    public async Task<Favori?> GetByUserAndLivreAsync(int utilisateurId, int livreId)
    {
        return await _favoriRepository.GetByUserAndLivreAsync(utilisateurId, livreId);
    }

    public async Task<Favori> AddAsync(int utilisateurId, int livreId)
    {
        var favori = new Favori
        {
            UtilisateurId = utilisateurId,
            LivreId = livreId,
            DateAjout = DateTime.Now
        };

        return await _favoriRepository.AddAsync(favori);
    }

    public async Task RemoveAsync(int favoriId)
    {
        await _favoriRepository.DeleteAsync(favoriId);
    }

    public async Task<bool> IsFavoriAsync(int utilisateurId, int livreId)
    {
        return await _favoriRepository.ExistsAsync(utilisateurId, livreId);
    }
}

