using BibliothequeWeb.Models;

namespace BibliothequeWeb.Services;

/// <summary>
/// Interface du service d'authentification
/// </summary>
public interface IAuthentificationService
{
    Task<Compte?> LoginAsync(string email, string motDePasse);
    Task<bool> RegisterAsync(Utilisateur utilisateur);
    bool IsAdmin(Compte compte);
    string GetUserRole(Compte compte);
}

