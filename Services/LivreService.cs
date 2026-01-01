using BibliothequeWeb.Models;
using BibliothequeWeb.Repositories;

namespace BibliothequeWeb.Services;

/// <summary>
/// Service pour la gestion des livres
/// </summary>
public class LivreService : ILivreService
{
    private readonly ILivreRepository _livreRepository;

    public LivreService(ILivreRepository livreRepository)
    {
        _livreRepository = livreRepository;
    }

    public async Task<Livre?> GetByIdAsync(int id)
    {
        return await _livreRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Livre>> GetAllAsync()
    {
        return await _livreRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Livre>> GetByCategorieIdAsync(int categorieId)
    {
        return await _livreRepository.GetByCategorieIdAsync(categorieId);
    }

    public async Task<IEnumerable<Livre>> SearchAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await GetAllAsync();

        return await _livreRepository.SearchAsync(searchTerm);
    }

    public async Task<Livre> CreateAsync(Livre livre)
    {
        return await _livreRepository.AddAsync(livre);
    }

    public async Task UpdateAsync(Livre livre)
    {
        await _livreRepository.UpdateAsync(livre);
    }

    public async Task DeleteAsync(int id)
    {
        await _livreRepository.DeleteAsync(id);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _livreRepository.ExistsAsync(id);
    }
}

