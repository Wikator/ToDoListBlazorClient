using System.Text;
using System.Text.Json;

namespace ToDoListBlazorClient.Helpers;

public static class JsonUtility
{
    public static StringContent GenerateSnakeCaseJson<TBody>(TBody body)
    {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };

        var requestBody = JsonSerializer.Serialize(body, jsonSerializerOptions);
        return new StringContent(requestBody, Encoding.UTF8, "application/json");
    }
}