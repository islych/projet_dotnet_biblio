using Microsoft.AspNetCore.Mvc;
using BibliothequeWeb.Repositories;
using BibliothequeWeb.Helpers;
using BibliothequeWeb.Models;
using BibliothequeWeb.Attributes;

namespace BibliothequeWeb.Controllers;

/// <summary>
/// Controller pour la gestion des utilisateurs (Admin)
/// </summary>
[AdminOnly]
public class UtilisateursController : Controller
{
    private readonly ICompteRepository _compteRepository;

    public UtilisateursController(ICompteRepository compteRepository)
    {
        _compteRepository = compteRepository;
    }

    public async Task<IActionResult> Index()
    {
        var comptes = await _compteRepository.GetAllAsync();
        return View(comptes);
    }

    public async Task<IActionResult> Details(int id)
    {
        var compte = await _compteRepository.GetByIdAsync(id);
        if (compte == null)
        {
            return NotFound();
        }

        return View(compte);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string typeCompte, Utilisateur utilisateur)
    {
        utilisateur.DateInscription = DateTime.Now;

        // Réinitialiser le ModelState pour les champs gérés manuellement
        ModelState.Remove("Discriminator");
        ModelState.Remove("Role");

        if (ModelState.IsValid)
        {
            // Vérifier si l'email existe déjà
            var existing = await _compteRepository.GetByEmailAsync(utilisateur.Email);
            if (existing != null)
            {
                ViewBag.Error = "Cet email est déjà utilisé";
                ViewBag.TypeCompte = typeCompte;
                return View(utilisateur);
            }

            Compte compteACreer;

            // Créer Admin ou Utilisateur selon le choix
            if (typeCompte == "Admin")
            {
                var admin = new Admin
                {
                    Nom = utilisateur.Nom,
                    Email = utilisateur.Email,
                    MotDePasse = utilisateur.MotDePasse,
                    DateInscription = DateTime.Now,
                    Discriminator = "Admin"
                };
                compteACreer = admin;
            }
            else
            {
                utilisateur.Discriminator = "Utilisateur";
                if (string.IsNullOrEmpty(utilisateur.Role))
                {
                    utilisateur.Role = "Utilisateur";
                }
                compteACreer = utilisateur;
            }

            await _compteRepository.AddAsync(compteACreer);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.TypeCompte = typeCompte;
        return View(utilisateur);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var compte = await _compteRepository.GetByIdAsync(id);
        if (compte == null)
        {
            return NotFound();
        }

        // Permettre l'édition des Utilisateurs
        if (compte is Utilisateur utilisateur)
        {
            ViewBag.TypeCompte = "Utilisateur";
            ViewBag.IsAdmin = false;
            return View(utilisateur);
        }
        
        // Pour les Admins, créer un modèle Utilisateur temporaire pour l'édition
        if (compte is Admin admin)
        {
            var utilisateurTemp = new Utilisateur
            {
                Id = admin.Id,
                Nom = admin.Nom,
                Email = admin.Email,
                MotDePasse = admin.MotDePasse,
                DateInscription = admin.DateInscription
            };
            ViewBag.TypeCompte = "Admin";
            ViewBag.IsAdmin = true;
            return View(utilisateurTemp);
        }

        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Utilisateur utilisateur, string typeCompte)
    {
        if (id != utilisateur.Id)
        {
            return NotFound();
        }

        // Récupérer le compte existant
        var existing = await _compteRepository.GetByIdAsync(id);
        if (existing == null)
        {
            return NotFound();
        }

        // Si le mot de passe est vide, conserver l'ancien
        if (string.IsNullOrWhiteSpace(utilisateur.MotDePasse))
        {
            utilisateur.MotDePasse = existing.MotDePasse;
            ModelState.Remove("MotDePasse");
        }

        utilisateur.DateInscription = existing.DateInscription;

        // Réinitialiser le ModelState
        ModelState.Remove("Discriminator");
        ModelState.Remove("Role");
        ModelState.Remove("DateInscription");

        if (ModelState.IsValid)
        {
            // Vérifier si l'email est utilisé par un autre compte
            var emailExists = await _compteRepository.GetByEmailAsync(utilisateur.Email);
            if (emailExists != null && emailExists.Id != utilisateur.Id)
            {
                ViewBag.Error = "Cet email est déjà utilisé par un autre compte";
                ViewBag.TypeCompte = typeCompte;
                return View(utilisateur);
            }

            try
            {
                // Si changement de type, supprimer l'ancien et créer le nouveau
                if ((typeCompte == "Admin" && existing.Discriminator != "Admin") ||
                    (typeCompte == "Utilisateur" && existing.Discriminator == "Admin"))
                {
                    // Supprimer l'ancien compte
                    await _compteRepository.DeleteAsync(existing.Id);
                    
                    // Créer le nouveau compte avec le bon type
                    Compte nouveauCompte;
                    if (typeCompte == "Admin")
                    {
                        nouveauCompte = new Admin
                        {
                            Nom = utilisateur.Nom,
                            Email = utilisateur.Email,
                            MotDePasse = utilisateur.MotDePasse,
                            DateInscription = existing.DateInscription,
                            Discriminator = "Admin"
                        };
                    }
                    else
                    {
                        utilisateur.Discriminator = "Utilisateur";
                        if (string.IsNullOrEmpty(utilisateur.Role))
                        {
                            utilisateur.Role = "Utilisateur";
                        }
                        utilisateur.DateInscription = existing.DateInscription;
                        nouveauCompte = utilisateur;
                    }
                    
                    await _compteRepository.AddAsync(nouveauCompte);
                }
                else
                {
                    // Pas de changement de type, juste mettre à jour
                    Compte compteAMettreAJour;
                    if (existing is Admin admin)
                    {
                        admin.Nom = utilisateur.Nom;
                        admin.Email = utilisateur.Email;
                        admin.MotDePasse = utilisateur.MotDePasse;
                        compteAMettreAJour = admin;
                    }
                    else
                    {
                        utilisateur.Discriminator = "Utilisateur";
                        if (string.IsNullOrEmpty(utilisateur.Role))
                        {
                            utilisateur.Role = (existing as Utilisateur)?.Role ?? "Utilisateur";
                        }
                        compteAMettreAJour = utilisateur;
                    }

                    await _compteRepository.UpdateAsync(compteAMettreAJour);
                }
                
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Une erreur est survenue : {ex.Message}";
                ViewBag.TypeCompte = typeCompte;
                return View(utilisateur);
            }
        }

        ViewBag.TypeCompte = typeCompte;
        return View(utilisateur);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var compte = await _compteRepository.GetByIdAsync(id);
        if (compte == null)
        {
            return NotFound();
        }

        // Empêcher la suppression d'un admin
        if (compte is Admin)
        {
            ViewBag.Error = "Les administrateurs ne peuvent pas être supprimés via cette interface";
            return RedirectToAction(nameof(Index));
        }

        return View(compte);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var compte = await _compteRepository.GetByIdAsync(id);
        if (compte == null)
        {
            return NotFound();
        }

        // Empêcher la suppression d'un admin
        if (compte is Admin)
        {
            ViewBag.Error = "Les administrateurs ne peuvent pas être supprimés";
            return RedirectToAction(nameof(Index));
        }

        await _compteRepository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}

