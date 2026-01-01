using BibliothequeWeb.Models;
using BibliothequeWeb.Repositories;

namespace BibliothequeWeb.Services;

/// <summary>
/// Service d'authentification pour gérer les connexions et inscriptions
/// </summary>

public class AuthentificationService : IAuthentificationService
{
    private readonly ICompteRepository _compteRepository;

    public AuthentificationService(ICompteRepository compteRepository)
    {
        _compteRepository = compteRepository;
    }

    public async Task<Compte?> LoginAsync(string email, string motDePasse)
    {
        var compte = await _compteRepository.GetByEmailAsync(email);
        if (compte == null)
            return null;

        // Vérification simple du mot de passe (en production, utiliser BCrypt ou similar)
        if (compte.MotDePasse == motDePasse)
        {
            return compte;
        }

        return null;
    }

    public async Task<bool> RegisterAsync(Utilisateur utilisateur)
    {
        // Vérifier si l'email existe déjà
        var existing = await _compteRepository.GetByEmailAsync(utilisateur.Email);
        if (existing != null)
            return false;

        // S'assurer que les propriétés requises sont définies
        utilisateur.Discriminator = "Utilisateur";
        utilisateur.DateInscription = DateTime.Now;
        
        // S'assurer que le Role est défini
        if (string.IsNullOrEmpty(utilisateur.Role))
        {
            utilisateur.Role = "Utilisateur";
        }

        try
        {
            var result = await _compteRepository.AddAsync(utilisateur);
            // Vérifier que l'ID a été généré (preuve que l'insertion a réussi)
            return result.Id > 0;
        }
        catch (Exception ex)
        {
            // En cas d'erreur (ex: contrainte unique, etc.)
            // Log l'erreur pour le débogage
            System.Diagnostics.Debug.WriteLine($"Erreur lors de l'inscription: {ex.Message}");
            return false;
        }
    }

    public bool IsAdmin(Compte compte)
    {
        // Vérifier à la fois le type et le Discriminator
        return compte is Admin || compte.Discriminator == "Admin";
    }

    public string GetUserRole(Compte compte)
    {
        // Vérifier le Discriminator pour déterminer le rôle
        if (compte.Discriminator == "Admin" || compte is Admin)
        {
            return "Admin";
        }
        return "Utilisateur";
    }
}

