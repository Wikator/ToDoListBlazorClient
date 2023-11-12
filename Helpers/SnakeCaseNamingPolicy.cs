using System.Text.Json;
using ToDoListBlazorClient.Extensions;

namespace ToDoListBlazorClient.Helpers;

public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public static SnakeCaseNamingPolicy Instance { get; } = new();

    public override string ConvertName(string name)
    {
        return name.ToSnakeCase();
    }
}