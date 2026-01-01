using Microsoft.AspNetCore.Mvc;
using BibliothequeWeb.Models;
using BibliothequeWeb.Services;
using BibliothequeWeb.Helpers;

namespace BibliothequeWeb.Controllers;

/// <summary>
/// Controller pour l'authentification (Login/Logout)
/// </summary>
public class AuthController : Controller
{
    private readonly IAuthentificationService _authService;

    public AuthController(IAuthentificationService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (SessionHelper.IsAuthenticated(HttpContext.Session))
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string email, string motDePasse)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(motDePasse))
        {
            ViewBag.Error = "Email et mot de passe sont requis";
            return View();
        }

        var compte = await _authService.LoginAsync(email, motDePasse);
        if (compte == null)
        {
            ViewBag.Error = "Email ou mot de passe incorrect";
            return View();
        }

        var role = _authService.GetUserRole(compte);
        SessionHelper.SetUserSession(HttpContext.Session, compte, role);

        if (role == "Admin")
        {
            return RedirectToAction("Dashboard", "Admin");
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult Logout()
    {
        SessionHelper.ClearUserSession(HttpContext.Session);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(Utilisateur utilisateur)
    {
        // S'assurer que le Role est défini
        if (string.IsNullOrEmpty(utilisateur.Role))
        {
            utilisateur.Role = "Utilisateur";
        }

        // S'assurer que le Discriminator est défini
        if (string.IsNullOrEmpty(utilisateur.Discriminator))
        {
            utilisateur.Discriminator = "Utilisateur";
        }

        // Réinitialiser le ModelState pour le Discriminator et Role car ils sont cachés
        ModelState.Remove("Discriminator");
        ModelState.Remove("Role");

        if (!ModelState.IsValid)
        {
            return View(utilisateur);
        }

        try
        {
            var success = await _authService.RegisterAsync(utilisateur);
            if (!success)
            {
                ViewBag.Error = "Cet email est déjà utilisé";
                return View(utilisateur);
            }

            ViewBag.Success = "Inscription réussie. Vous pouvez maintenant vous connecter.";
            return View("Login");
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Une erreur est survenue lors de l'inscription : {ex.Message}";
            return View(utilisateur);
        }
    }
}

