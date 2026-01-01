using BibliothequeWeb.Models;

namespace BibliothequeWeb.Repositories;

/// <summary>
/// Interface du repository pour la gestion des comptes
/// </summary>
public interface ICompteRepository
{
    Task<Compte?> GetByIdAsync(int id);
    Task<Compte?> GetByEmailAsync(string email);
    Task<IEnumerable<Compte>> GetAllAsync();
    Task<Compte> AddAsync(Compte compte);
    Task UpdateAsync(Compte compte);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
