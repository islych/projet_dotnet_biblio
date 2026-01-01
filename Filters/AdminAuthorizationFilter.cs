using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BibliothequeWeb.Helpers;

namespace BibliothequeWeb.Filters;

/// <summary>
/// Filtre d'autorisation personnalisé pour vérifier les droits administrateur via les sessions
/// </summary>
public class AdminAuthorizationFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!SessionHelper.IsAuthenticated(context.HttpContext.Session))
        {
            context.Result = new RedirectToActionResult("Login", "Auth", null);
            return;
        }

        if (!SessionHelper.IsAdmin(context.HttpContext.Session))
        {
            context.Result = new ForbidResult();
            return;
        }
    }
}

