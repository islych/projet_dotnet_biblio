using BibliothequeWeb.Models;
using BibliothequeWeb.Repositories;

namespace BibliothequeWeb.Services;

/// <summary>
/// Service pour la gestion des catégories
/// </summary>
public class CategorieService : ICategorieService
{
    private readonly ICategorieRepository _categorieRepository;

    public CategorieService(ICategorieRepository categorieRepository)
    {
        _categorieRepository = categorieRepository;
    }

    public async Task<Categorie?> GetByIdAsync(int id)
    {
        return await _categorieRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Categorie>> GetAllAsync()
    {
        return await _categorieRepository.GetAllAsync();
    }

    public async Task<Categorie> CreateAsync(Categorie categorie)
    {
        return await _categorieRepository.AddAsync(categorie);
    }

    public async Task UpdateAsync(Categorie categorie)
    {
        await _categorieRepository.UpdateAsync(categorie);
    }

    public async Task DeleteAsync(int id)
    {
        await _categorieRepository.DeleteAsync(id);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _categorieRepository.ExistsAsync(id);
    }
}

