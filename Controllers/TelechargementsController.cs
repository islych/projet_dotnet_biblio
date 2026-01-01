using Microsoft.AspNetCore.Mvc;
using BibliothequeWeb.Services;
using BibliothequeWeb.Helpers;

namespace BibliothequeWeb.Controllers;

/// <summary>
/// Controller pour la gestion des téléchargements
/// </summary>
public class TelechargementsController : Controller
{
    private readonly ITelechargementService _telechargementService;
    private readonly ILivreService _livreService;

    public TelechargementsController(
        ITelechargementService telechargementService,
        ILivreService livreService)
    {
        _telechargementService = telechargementService;
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

        var telechargements = await _telechargementService.GetByUserIdAsync(userId.Value);
        return View(telechargements);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Download(int livreId)
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

        var livre = await _livreService.GetByIdAsync(livreId);
        if (livre == null || string.IsNullOrEmpty(livre.FichierPdf))
        {
            return NotFound();
        }

        // Enregistrer le téléchargement
        var adresseIp = HttpContext.Connection.RemoteIpAddress?.ToString();
        await _telechargementService.RecordDownloadAsync(userId.Value, livreId, adresseIp);

        // Retourner le fichier
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", livre.FichierPdf.TrimStart('/'));
        if (!System.IO.File.Exists(filePath))
        {
            return NotFound();
        }

        var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
        var fileName = Path.GetFileName(filePath);

        return File(fileBytes, "application/pdf", fileName);
    }
}

