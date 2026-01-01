using BibliothequeWeb.Data;
using BibliothequeWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliothequeWeb.Repositories;

/// <summary>
/// Implémentation du repository pour la gestion des livres
/// </summary>
public class LivreRepository : ILivreRepository
{
    private readonly ApplicationDbContext _context;

    public LivreRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Livre?> GetByIdAsync(int id)
    {
        return await _context.Livres
            .Include(l => l.Categorie)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<IEnumerable<Livre>> GetAllAsync()
    {
        return await _context.Livres
            .Include(l => l.Categorie)
            .OrderBy(l => l.Titre)
            .ToListAsync();
    }

    public async Task<IEnumerable<Livre>> GetByCategorieIdAsync(int categorieId)
    {
        return await _context.Livres
            .Include(l => l.Categorie)
            .Where(l => l.CategorieId == categorieId)
            .OrderBy(l => l.Titre)
            .ToListAsync();
    }

    public async Task<IEnumerable<Livre>> SearchAsync(string searchTerm)
    {
        return await _context.Livres
            .Include(l => l.Categorie)
            .Where(l => l.Titre.Contains(searchTerm) || 
                       (l.Auteur != null && l.Auteur.Contains(searchTerm)) ||
                       (l.Description != null && l.Description.Contains(searchTerm)))
            .OrderBy(l => l.Titre)
            .ToListAsync();
    }

    public async Task<Livre> AddAsync(Livre livre)
    {
        _context.Livres.Add(livre);
        await _context.SaveChangesAsync();
        return livre;
    }

    public async Task UpdateAsync(Livre livre)
    {
        _context.Livres.Update(livre);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var livre = await GetByIdAsync(id);
        if (livre != null)
        {
            _context.Livres.Remove(livre);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Livres.AnyAsync(l => l.Id == id);
    }
}

