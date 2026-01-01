using BibliothequeWeb.Data;
using BibliothequeWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliothequeWeb.Repositories;

/// <summary>
/// Implémentation du repository pour la gestion des catégories
/// </summary>
public class CategorieRepository : ICategorieRepository
{
    private readonly ApplicationDbContext _context;

    public CategorieRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Categorie?> GetByIdAsync(int id)
    {
        return await _context.Categories
            .Include(c => c.Livres)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Categorie>> GetAllAsync()
    {
        return await _context.Categories
            .OrderBy(c => c.Nom)
            .ToListAsync();
    }

    public async Task<Categorie> AddAsync(Categorie categorie)
    {
        _context.Categories.Add(categorie);
        await _context.SaveChangesAsync();
        return categorie;
    }

    public async Task UpdateAsync(Categorie categorie)
    {
        _context.Categories.Update(categorie);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var categorie = await GetByIdAsync(id);
        if (categorie != null)
        {
            _context.Categories.Remove(categorie);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Categories.AnyAsync(c => c.Id == id);
    }
}

