using BibliothequeWeb.Data;
using BibliothequeWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliothequeWeb.Repositories;

/// <summary>
/// Implémentation du repository pour la gestion des comptes
/// </summary>
public class CompteRepository : ICompteRepository
{
    private readonly ApplicationDbContext _context;

    public CompteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Compte?> GetByIdAsync(int id)
    {
        return await _context.Comptes.FindAsync(id);
    }

    public async Task<Compte?> GetByEmailAsync(string email)
    {
        return await _context.Comptes
            .FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<IEnumerable<Compte>> GetAllAsync()
    {
        return await _context.Comptes.ToListAsync();
    }

    public async Task<Compte> AddAsync(Compte compte)
    {
        // Pour l'héritage TPH, utiliser le DbSet approprié selon le type
        if (compte is Utilisateur utilisateur)
        {
            _context.Utilisateurs.Add(utilisateur);
        }
        else if (compte is Admin admin)
        {
            _context.Admins.Add(admin);
        }
        else
        {
            _context.Comptes.Add(compte);
        }
        
        await _context.SaveChangesAsync();
        return compte;
    }

    public async Task UpdateAsync(Compte compte)
    {
        _context.Comptes.Update(compte);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var compte = await GetByIdAsync(id);
        if (compte != null)
        {
            _context.Comptes.Remove(compte);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Comptes.AnyAsync(c => c.Id == id);
    }
}

