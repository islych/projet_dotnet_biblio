using Microsoft.AspNetCore.Mvc;
using BibliothequeWeb.Models;
using BibliothequeWeb.Services;
using BibliothequeWeb.Helpers;
using BibliothequeWeb.Attributes;

namespace BibliothequeWeb.Controllers;

/// <summary>
/// Controller pour la gestion des livres (CRUD Admin)
/// </summary>
[AdminOnly]
public class LivresController : Controller
{
    private readonly ILivreService _livreService;
    private readonly ICategorieService _categorieService;

    public LivresController(ILivreService livreService, ICategorieService categorieService)
    {
        _livreService = livreService;
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

        var livres = await _livreService.GetAllAsync();
        return View(livres);
    }

    public async Task<IActionResult> Details(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Auth");
        }

        var livre = await _livreService.GetByIdAsync(id);
        if (livre == null)
        {
            return NotFound();
        }

        return View(livre);
    }

    public async Task<IActionResult> Create()
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Auth");
        }

        var categories = await _categorieService.GetAllAsync();
        ViewBag.Categories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(categories, "Id", "Nom");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Livre livre, IFormFile? fichierPdf, IFormFile? imageCouverture)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Auth");
        }

        if (ModelState.IsValid)
        {
            // Gestion de l'upload du fichier PDF
            if (fichierPdf != null && fichierPdf.Length > 0)
            {
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(fichierPdf.FileName);
                var filePath = Path.Combine(uploadsPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await fichierPdf.CopyToAsync(stream);
                }
                livre.FichierPdf = $"/uploads/{fileName}";
            }

            // Gestion de l'upload de l'image
            if (imageCouverture != null && imageCouverture.Length > 0)
            {
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageCouverture.FileName);
                var filePath = Path.Combine(uploadsPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageCouverture.CopyToAsync(stream);
                }
                livre.ImageCouverture = $"/images/{fileName}";
            }

            await _livreService.CreateAsync(livre);
            return RedirectToAction(nameof(Index));
        }

        var categories = await _categorieService.GetAllAsync();
        ViewBag.Categories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(categories, "Id", "Nom", livre.CategorieId);
        return View(livre);
    }

    public async Task<IActionResult> Edit(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Auth");
        }

        var livre = await _livreService.GetByIdAsync(id);
        if (livre == null)
        {
            return NotFound();
        }

        var categories = await _categorieService.GetAllAsync();
        ViewBag.Categories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(categories, "Id", "Nom", livre.CategorieId);
        return View(livre);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Livre livre, IFormFile? fichierPdf, IFormFile? imageCouverture)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Auth");
        }

        if (id != livre.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            // Gestion de l'upload du fichier PDF
            if (fichierPdf != null && fichierPdf.Length > 0)
            {
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(fichierPdf.FileName);
                var filePath = Path.Combine(uploadsPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await fichierPdf.CopyToAsync(stream);
                }
                livre.FichierPdf = $"/uploads/{fileName}";
            }

            // Gestion de l'upload de l'image
            if (imageCouverture != null && imageCouverture.Length > 0)
            {
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageCouverture.FileName);
                var filePath = Path.Combine(uploadsPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageCouverture.CopyToAsync(stream);
                }
                livre.ImageCouverture = $"/images/{fileName}";
            }

            await _livreService.UpdateAsync(livre);
            return RedirectToAction(nameof(Index));
        }

        var categories = await _categorieService.GetAllAsync();
        ViewBag.Categories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(categories, "Id", "Nom", livre.CategorieId);
        return View(livre);
    }

    public async Task<IActionResult> Delete(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Auth");
        }

        var livre = await _livreService.GetByIdAsync(id);
        if (livre == null)
        {
            return NotFound();
        }

        return View(livre);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Login", "Auth");
        }

        await _livreService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}

