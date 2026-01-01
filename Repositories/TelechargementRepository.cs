using BibliothequeWeb.Data;
using BibliothequeWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliothequeWeb.Repositories;

/// <summary>
/// Implémentation du repository pour la gestion des téléchargements
/// </summary>
public class TelechargementRepository : ITelechargementRepository
{
    private readonly ApplicationDbContext _context;

    public TelechargementRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Telechargement?> GetByIdAsync(int id)
    {
        return await _context.Telechargements
            .Include(t => t.Utilisateur)
            .Include(t => t.Livre)
            .ThenInclude(l => l!.Categorie)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Telechargement>> GetByUserIdAsync(int utilisateurId)
    {
        return await _context.Telechargements
            .Include(t => t.Livre)
            .ThenInclude(l => l!.Categorie)
            .Where(t => t.UtilisateurId == utilisateurId)
            .OrderByDescending(t => t.DateTelechargement)
            .ToListAsync();
    }

    public async Task<Telechargement> AddAsync(Telechargement telechargement)
    {
        _context.Telechargements.Add(telechargement);
        await _context.SaveChangesAsync();
        return telechargement;
    }

    public async Task<bool> ExistsAsync(int utilisateurId, int livreId)
    {
        return await _context.Telechargements
            .AnyAsync(t => t.UtilisateurId == utilisateurId && t.LivreId == livreId);
    }
}

