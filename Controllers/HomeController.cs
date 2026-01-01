using Microsoft.AspNetCore.Mvc;
using BibliothequeWeb.Services;
using BibliothequeWeb.Helpers;

namespace BibliothequeWeb.Controllers;

/// <summary>
/// Controller principal pour la page d'accueil et le catalogue
/// </summary>
public class HomeController : Controller
{
    private readonly ILivreService _livreService;
    private readonly ICategorieService _categorieService;

    public HomeController(ILivreService livreService, ICategorieService categorieService)
    {
        _livreService = livreService;
        _categorieService = categorieService;
    }

    public async Task<IActionResult> Index(string? search, int? categorieId)
    {
        ViewBag.Categories = await _categorieService.GetAllAsync();

        IEnumerable<Models.Livre> livres;

        if (!string.IsNullOrWhiteSpace(search))
        {
            livres = await _livreService.SearchAsync(search);
            ViewBag.SearchTerm = search;
        }
        else if (categorieId.HasValue)
        {
            livres = await _livreService.GetByCategorieIdAsync(categorieId.Value);
            ViewBag.CategorieId = categorieId;
        }
        else
        {
            livres = await _livreService.GetAllAsync();
        }

        return View(livres);
    }

    public async Task<IActionResult> Details(int id)
    {
        var livre = await _livreService.GetByIdAsync(id);
        if (livre == null)
        {
            return NotFound();
        }

        ViewBag.IsAuthenticated = SessionHelper.IsAuthenticated(HttpContext.Session);
        if (ViewBag.IsAuthenticated)
        {
            ViewBag.UserId = SessionHelper.GetUserId(HttpContext.Session);
        }

        return View(livre);
    }
}

