using System.Net.Http.Json;

namespace ToDoListBlazorClient.Models;

public class Response<T>
{
    public required string Message { get; init; }
    public required bool IsSuccess { get; init; }
    public T? Data { get; private init; }

    public static async Task<Response<T>> GenerateSuccessfulResponseAsync(HttpContent content)
    {
        var payload = await content.ReadFromJsonAsync<T>();

        if (payload is null)
            return await GenerateFailedResponseAsync(content);

        return new Response<T>
        {
            IsSuccess = true,
            Data = payload,
            Message = "Operation completed successfully."
        };
    }

    public static async Task<Response<T>> GenerateFailedResponseAsync(HttpContent httpContent)
    {
        var message = await httpContent.ReadAsStringAsync();

        return new Response<T>
        {
            IsSuccess = false,
            Message = string.IsNullOrEmpty(message) ? "Something went wrong. Please try again later." : message
        };
    }
}

public class Response
{
    public required string Message { get; init; }
    public required bool IsSuccess { get; init; }

    public static Response GenerateSuccessfulResponse()
    {
        return new Response
        {
            IsSuccess = true,
            Message = "Operation completed successfully."
        };
    }

    public static async Task<Response> GenerateFailedResponseAsync(HttpContent httpContent)
    {
        var message = await httpContent.ReadAsStringAsync();

        return new Response
        {
            IsSuccess = false,
            Message = string.IsNullOrEmpty(message) ? "Something went wrong. Please try again later." : message
        };
    }
}