using BibliothequeWeb.Models;

namespace BibliothequeWeb.Helpers;

/// <summary>
/// Helper pour gérer les sessions utilisateur
/// </summary>
public static class SessionHelper
{
    private const string UserIdKey = "UserId";
    private const string UserEmailKey = "UserEmail";
    private const string UserNameKey = "UserName";
    private const string UserRoleKey = "UserRole";

    public static void SetUserSession(ISession session, Compte compte, string role)
    {
        session.SetInt32(UserIdKey, compte.Id);
        session.SetString(UserEmailKey, compte.Email);
        session.SetString(UserNameKey, compte.Nom);
        session.SetString(UserRoleKey, role);
    }

    public static void ClearUserSession(ISession session)
    {
        session.Remove(UserIdKey);
        session.Remove(UserEmailKey);
        session.Remove(UserNameKey);
        session.Remove(UserRoleKey);
    }

    public static int? GetUserId(ISession session)
    {
        return session.GetInt32(UserIdKey);
    }

    public static string? GetUserEmail(ISession session)
    {
        return session.GetString(UserEmailKey);
    }

    public static string? GetUserName(ISession session)
    {
        return session.GetString(UserNameKey);
    }

    public static string? GetUserRole(ISession session)
    {
        return session.GetString(UserRoleKey);
    }

    public static bool IsAuthenticated(ISession session)
    {
        return GetUserId(session).HasValue;
    }

    public static bool IsAdmin(ISession session)
    {
        return GetUserRole(session) == "Admin";
    }
}

