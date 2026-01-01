using Microsoft.AspNetCore.Mvc;
using BibliothequeWeb.Services;
using BibliothequeWeb.Helpers;

namespace BibliothequeWeb.Controllers;

/// <summary>
/// Controller pour la gestion des favoris
/// </summary>
public class FavorisController : Controller
{
    private readonly IFavoriService _favoriService;
    private readonly ILivreService _livreService;

    public FavorisController(IFavoriService favoriService, ILivreService livreService)
    {
        _favoriService = favoriService;
        _livreService = livreService;
    }

    public async Task<IActionResult> Index()
    {
        if (!SessionHelper.IsAuthenticated(HttpContext.Session))
        {
            return RedirectToAction("Login", "Auth");
        }

        var userId = SessionHelper.GetUserId(HttpContext.Session);
        if (!userId.HasValue)
        {
            return RedirectToAction("Login", "Auth");
        }

        var favoris = await _favoriService.GetByUserIdAsync(userId.Value);
        return View(favoris);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int livreId)
    {
        if (!SessionHelper.IsAuthenticated(HttpContext.Session))
        {
            return Json(new { success = false, message = "Non authentifié" });
        }

        var userId = SessionHelper.GetUserId(HttpContext.Session);
        if (!userId.HasValue)
        {
            return Json(new { success = false, message = "Utilisateur non trouvé" });
        }

        var livre = await _livreService.GetByIdAsync(livreId);
        if (livre == null)
        {
            return Json(new { success = false, message = "Livre non trouvé" });
        }

        var exists = await _favoriService.IsFavoriAsync(userId.Value, livreId);
        if (exists)
        {
            return Json(new { success = false, message = "Déjà en favoris" });
        }

        await _favoriService.AddAsync(userId.Value, livreId);
        return Json(new { success = true, message = "Ajouté aux favoris" });
    }

    [HttpGet]
    public async Task<IActionResult> Check(int livreId)
    {
        if (!SessionHelper.IsAuthenticated(HttpContext.Session))
        {
            return Json(new { isFavori = false });
        }

        var userId = SessionHelper.GetUserId(HttpContext.Session);
        if (!userId.HasValue)
        {
            return Json(new { isFavori = false });
        }

        var isFavori = await _favoriService.IsFavoriAsync(userId.Value, livreId);
        if (isFavori)
        {
            var favori = await _favoriService.GetByUserAndLivreAsync(userId.Value, livreId);
            return Json(new { isFavori = true, favoriId = favori?.Id });
        }

        return Json(new { isFavori = false });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(int favoriId)
    {
        if (!SessionHelper.IsAuthenticated(HttpContext.Session))
        {
            return Json(new { success = false, message = "Non authentifié" });
        }

        await _favoriService.RemoveAsync(favoriId);
        return Json(new { success = true, message = "Retiré des favoris" });
    }
}

