using BibliothequeWeb.Models;

namespace BibliothequeWeb.Repositories;

/// <summary>
/// Interface du repository pour la gestion des téléchargements
/// </summary>
public interface ITelechargementRepository
{
    Task<Telechargement?> GetByIdAsync(int id);
    Task<IEnumerable<Telechargement>> GetByUserIdAsync(int utilisateurId);
    Task<Telechargement> AddAsync(Telechargement telechargement);
    Task<bool> ExistsAsync(int utilisateurId, int livreId);
}

