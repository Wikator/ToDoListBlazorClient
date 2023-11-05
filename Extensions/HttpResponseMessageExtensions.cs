using System.Net.Http.Json;

namespace ToDoListBlazorClient.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task<T> ReadJsonFromBodyAsync<T>(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
            throw new Exception(await response.Content.ReadAsStringAsync());

        return await response.Content.ReadFromJsonAsync<T>()
               ?? throw new Exception("Error parsing json");
    }
}