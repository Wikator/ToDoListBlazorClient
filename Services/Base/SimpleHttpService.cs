using Blazored.LocalStorage;
using ToDoListBlazorClient.Extensions;
using ToDoListBlazorClient.Helpers;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services.Base;

public abstract class SimpleHttpService<TResponse, TBody>(ILocalStorageService localStorage,
        HttpClient httpClient)
    : ISimpleHttpService<TResponse, TBody>
{
    private ILocalStorageService LocalStorage { get; } = localStorage;
    private HttpClient HttpClient { get; } = httpClient;
    
    protected abstract string BaseUrl { get; }

    public async Task<Response<IEnumerable<TResponse>>> SimpleGetAsync()
    {
        await HttpClient.AddJwtTokenAsync(LocalStorage);
        var response = await HttpClient.GetAsync(BaseUrl);
        return await GenerateResponseAsync<IEnumerable<TResponse>>(response);
    }

    public async Task<Response<TResponse>> SimpleGetAsync(int id)
    {
        await HttpClient.AddJwtTokenAsync(LocalStorage);
        var response = await HttpClient.GetAsync(BaseUrl + id);
        return await GenerateResponseAsync<TResponse>(response);
    }

    public async Task<Response<TResponse>> SimplePostAsync(TBody body)
    {
        await HttpClient.AddJwtTokenAsync(LocalStorage);
        var stringContent = JsonUtility.GenerateSnakeCaseJson(body);
        var response = await HttpClient.PostAsync(BaseUrl, stringContent);
        return await GenerateResponseAsync<TResponse>(response);
    }

    public async Task<Response<TResponse>> SimplePutAsync(int id, TBody body)
    {
        await HttpClient.AddJwtTokenAsync(LocalStorage);
        var stringContent = JsonUtility.GenerateSnakeCaseJson(body);
        var response = await HttpClient.PutAsync(BaseUrl + id, stringContent);
        return await GenerateResponseAsync<TResponse>(response);
    }

    public async Task<Response> SimpleDeleteAsync(int id)
    {
        await HttpClient.AddJwtTokenAsync(LocalStorage);
        var response = await HttpClient.DeleteAsync(BaseUrl + id);
        return await GenerateResponseAsync(response);
    }

    private static async Task<Response<T>> GenerateResponseAsync<T>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
            return await Response<T>.GenerateFailedResponseAsync(response.Content);

        return await Response<T>.GenerateSuccessfulResponseAsync(response.Content);
    }

    private static async Task<Response> GenerateResponseAsync(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
            return await Response.GenerateFailedResponseAsync(response.Content);

        return Response.GenerateSuccessfulResponse();
    }
}