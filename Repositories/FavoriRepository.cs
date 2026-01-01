using BibliothequeWeb.Data;
using BibliothequeWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliothequeWeb.Repositories;

/// <summary>
/// Implémentation du repository pour la gestion des favoris
/// </summary>
public class FavoriRepository : IFavoriRepository
{
    private readonly ApplicationDbContext _context;

    public FavoriRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Favori?> GetByIdAsync(int id)
    {
        return await _context.Favoris
            .Include(f => f.Utilisateur)
            .Include(f => f.Livre)
            .ThenInclude(l => l!.Categorie)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Favori?> GetByUserAndLivreAsync(int utilisateurId, int livreId)
    {
        return await _context.Favoris
            .Include(f => f.Utilisateur)
            .Include(f => f.Livre)
            .FirstOrDefaultAsync(f => f.UtilisateurId == utilisateurId && f.LivreId == livreId);
    }

    public async Task<IEnumerable<Favori>> GetByUserIdAsync(int utilisateurId)
    {
        return await _context.Favoris
            .Include(f => f.Livre)
            .ThenInclude(l => l!.Categorie)
            .Where(f => f.UtilisateurId == utilisateurId)
            .OrderByDescending(f => f.DateAjout)
            .ToListAsync();
    }

    public async Task<Favori> AddAsync(Favori favori)
    {
        _context.Favoris.Add(favori);
        await _context.SaveChangesAsync();
        return favori;
    }

    public async Task DeleteAsync(int id)
    {
        var favori = await GetByIdAsync(id);
        if (favori != null)
        {
            _context.Favoris.Remove(favori);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int utilisateurId, int livreId)
    {
        return await _context.Favoris
            .AnyAsync(f => f.UtilisateurId == utilisateurId && f.LivreId == livreId);
    }
}

