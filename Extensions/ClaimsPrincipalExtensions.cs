using System.Security.Claims;

namespace ToDoListBlazorClient.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetEmail(this ClaimsPrincipal principal)
    {
        return principal.FindFirst(c => c.Type == "email")?.Value;
    }
}