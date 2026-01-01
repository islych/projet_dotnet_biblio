using Microsoft.AspNetCore.Mvc;
using BibliothequeWeb.Models;
using BibliothequeWeb.Services;
using BibliothequeWeb.Helpers;

namespace BibliothequeWeb.Controllers;

/// <summary>
/// Controller pour la gestion des catégories (CRUD Admin)
/// </summary>
public class CategoriesController : Controller
{
    private readonly ICategorieService _categorieService;

    public CategoriesController(ICategorieService categorieService)
    {
        _categorieService = categorieService;
    }

    private bool IsAdmin()
    {
        return SessionHelper.IsAdmin(HttpContext.Session);
    }

    public async Task<IActionResult> Index()
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Auth");
        }

        var categories = await _categorieService.GetAllAsync();
        return View(categories);
    }

    public async Task<IActionResult> Details(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Auth");
        }

        var categorie = await _categorieService.GetByIdAsync(id);
        if (categorie == null)
        {
            return NotFound();
        }

        return View(categorie);
    }

    public IActionResult Create()
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Auth");
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Categorie categorie)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Auth");
        }

        if (ModelState.IsValid)
        {
            await _categorieService.CreateAsync(categorie);
            return RedirectToAction(nameof(Index));
        }

        return View(categorie);
    }

    public async Task<IActionResult> Edit(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Auth");
        }

        var categorie = await _categorieService.GetByIdAsync(id);
        if (categorie == null)
        {
            return NotFound();
        }

        return View(categorie);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Categorie categorie)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Auth");
        }

        if (id != categorie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _categorieService.UpdateAsync(categorie);
            return RedirectToAction(nameof(Index));
        }

        return View(categorie);
    }

    public async Task<IActionResult> Delete(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Auth");
        }

        var categorie = await _categorieService.GetByIdAsync(id);
        if (categorie == null)
        {
            return NotFound();
        }

        return View(categorie);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Auth");
        }

        await _categorieService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}

