using System.Security.Claims;

namespace ToDoListBlazorClient.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetEmail(this ClaimsPrincipal principal)
    {
        return principal.FindFirst(c => c.Type == "email")?.Value;
    }

    public static int? GetId(this ClaimsPrincipal principal)
    {
        return principal.FindFirst(c => c.Type == "id")?.Value is { } id ? int.Parse(id) : null;
    }

    public static int? GetGroup(this ClaimsPrincipal principal)
    {
        var groupId = principal.FindFirst(c => c.Type == "group")?.Value;
        return string.IsNullOrEmpty(groupId) ? null : int.Parse(groupId);
    }
}