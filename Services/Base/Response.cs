using System.Net.Http.Json;

namespace ToDoListBlazorClient.Services.Base;

public class Response<T>
{
    public string? Message { get; private init; }
    public bool IsSuccess { get; private init; } = true;
    public T? Data { get; private init; }

    public static async Task<Response<T>> GenerateSuccessfulResponseAsync(HttpContent? content)
    {
        if (content is null)
            return await GenerateFailedResponseAsync(content);

        var payload = await content.ReadFromJsonAsync<T>();

        if (payload is null)
            return await GenerateFailedResponseAsync(content);

        return new Response<T>
        {
            Data = payload
        };
    }

    public static async Task<Response<T>> GenerateFailedResponseAsync(HttpContent? httpContent)
    {
        return new Response<T>
        {
            IsSuccess = false,
            Message = httpContent is null
                ? "Something went wrong"
                : await httpContent.ReadAsStringAsync()
        };
    }
}

public class Response
{
    public string? Message { get; private init; }
    public bool IsSuccess { get; private init; } = true;

    public static async Task<Response> GenerateFailedResponseAsync(HttpContent? httpContent)
    {
        return new Response
        {
            IsSuccess = false,
            Message = httpContent is null
                ? "Something went wrong"
                : await httpContent.ReadAsStringAsync()
        };
    }
}