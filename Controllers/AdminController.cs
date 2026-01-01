using Microsoft.AspNetCore.Mvc;
using BibliothequeWeb.Services;
using BibliothequeWeb.Helpers;
using BibliothequeWeb.Models;
using BibliothequeWeb.Attributes;
using BibliothequeWeb.Repositories;

namespace BibliothequeWeb.Controllers;

/// <summary>
/// Controller pour les fonctionnalités administrateur
/// </summary>
[AdminOnly]
public class AdminController : Controller
{
    private readonly ILivreService _livreService;
    private readonly ICategorieService _categorieService;
    private readonly ICompteRepository _compteRepository;

    public AdminController(
        ILivreService livreService,
        ICategorieService categorieService,
        ICompteRepository compteRepository)
    {
        _livreService = livreService;
        _categorieService = categorieService;
        _compteRepository = compteRepository;
    }

    // Vérification manuelle de l'admin (car on utilise les sessions)
    private bool IsAdmin()
    {
        return SessionHelper.IsAdmin(HttpContext.Session);
    }

    public async Task<IActionResult> Dashboard()
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Auth");
        }

        ViewBag.TotalLivres = (await _livreService.GetAllAsync()).Count();
        ViewBag.TotalCategories = (await _categorieService.GetAllAsync()).Count();
        ViewBag.TotalUtilisateurs = (await _compteRepository.GetAllAsync()).Count();

        return View();
    }
}

