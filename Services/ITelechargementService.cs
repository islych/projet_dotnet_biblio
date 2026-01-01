using BibliothequeWeb.Models;

namespace BibliothequeWeb.Services;

/// <summary>
/// Interface du service pour la gestion des téléchargements
/// </summary>
public interface ITelechargementService
{
    Task<IEnumerable<Telechargement>> GetByUserIdAsync(int utilisateurId);
    Task<Telechargement> RecordDownloadAsync(int utilisateurId, int livreId, string? adresseIp);
}

