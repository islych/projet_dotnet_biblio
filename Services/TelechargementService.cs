using BibliothequeWeb.Models;
using BibliothequeWeb.Repositories;

namespace BibliothequeWeb.Services;

/// <summary>
/// Service pour la gestion des téléchargements
/// </summary>
public class TelechargementService : ITelechargementService
{
    private readonly ITelechargementRepository _telechargementRepository;

    public TelechargementService(ITelechargementRepository telechargementRepository)
    {
        _telechargementRepository = telechargementRepository;
    }

    public async Task<IEnumerable<Telechargement>> GetByUserIdAsync(int utilisateurId)
    {
        return await _telechargementRepository.GetByUserIdAsync(utilisateurId);
    }

    public async Task<Telechargement> RecordDownloadAsync(int utilisateurId, int livreId, string? adresseIp)
    {
        var telechargement = new Telechargement
        {
            UtilisateurId = utilisateurId,
            LivreId = livreId,
            DateTelechargement = DateTime.Now,
            AdresseIp = adresseIp
        };

        return await _telechargementRepository.AddAsync(telechargement);
    }
}

