using Microsoft.AspNetCore.Mvc;
using BibliothequeWeb.Filters;

namespace BibliothequeWeb.Attributes;

/// <summary>
/// Attribut pour protéger les actions nécessitant les droits administrateur
/// </summary>
public class AdminOnlyAttribute : TypeFilterAttribute
{
    public AdminOnlyAttribute() : base(typeof(AdminAuthorizationFilter))
    {
    }
}

