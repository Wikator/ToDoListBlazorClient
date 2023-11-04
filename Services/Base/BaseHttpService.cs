using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;

namespace ToDoListBlazorClient.Services.Base;

public abstract class BaseHttpService
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _httpClient;

    protected BaseHttpService(ILocalStorageService localStorage, HttpClient httpClient)
    {
        _localStorage = localStorage;
        _httpClient = httpClient;
    }

    protected async Task AddJwtTokenAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("accessToken");

        _httpClient.DefaultRequestHeaders.Authorization = token switch
        {
            null => null,
            _ => new AuthenticationHeaderValue("Bearer", token)
        };
    }

    protected static async Task<Response<T>> GenerateFailedResponseAsync<T>(HttpContent? httpContent)
    {
        return new Response<T>
        {
            IsSuccess = false,
            Message = httpContent is null
                ? "Something went wrong"
                : await httpContent.ReadAsStringAsync()
        };
    }
    
    protected static async Task<Response> GenerateFailedResponseAsync(HttpContent? httpContent)
    {
        return new Response
        {
            IsSuccess = false,
            Message = httpContent is null
                ? "Something went wrong"
                : await httpContent.ReadAsStringAsync()
        };
    }

    protected static async Task<Response<T>> GenerateSuccessfulResponseAsync<T>(HttpContent? content)
    {
        if (content is null)
            return await GenerateFailedResponseAsync<T>(content);
        
        var payload = await content.ReadFromJsonAsync<T>();

        if (payload is null)
            return await GenerateFailedResponseAsync<T>(content);

        return new Response<T>
        {
            Data = payload
        };
    }
    
    protected async Task<Response<T>> SimpleGetAsync<T>(string url = "")
    {
        await AddJwtTokenAsync();
        var response = await _httpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode)
            return await GenerateFailedResponseAsync<T>(response.Content);

        return await GenerateSuccessfulResponseAsync<T>(response.Content);
    }
    
    protected async Task<Response<T>> SimplePostAsync<T, TBody>(TBody body, string url = "")
    {
        await AddJwtTokenAsync();
        var response = await _httpClient.PostAsJsonAsync(url, body);
        
        if (!response.IsSuccessStatusCode)
            return await GenerateFailedResponseAsync<T>(response.Content);

        return await GenerateSuccessfulResponseAsync<T>(response.Content);
    }
    
    protected async Task<Response> SimplePostAsync<TBody>(TBody body, string url = "")
    {
        await AddJwtTokenAsync();
        var response = await _httpClient.PostAsJsonAsync(url, body);
        
        if (!response.IsSuccessStatusCode)
            return await GenerateFailedResponseAsync(response.Content);

        return new Response();
    }
    
    protected async Task<Response<T>> SimplePutAsync<T, TBody>(TBody body, string url = "")
    {
        await AddJwtTokenAsync();
        var response = await _httpClient.PutAsJsonAsync(url, body);
        
        if (!response.IsSuccessStatusCode)
            return await GenerateFailedResponseAsync<T>(response.Content);

        return await GenerateSuccessfulResponseAsync<T>(response.Content);
    }
    
    protected async Task<Response> SimplePutAsync<TBody>(TBody body, string url = "")
    {
        await AddJwtTokenAsync();
        var response = await _httpClient.PutAsJsonAsync(url, body);
        
        if (!response.IsSuccessStatusCode)
            return await GenerateFailedResponseAsync(response.Content);

        return new Response();
    }
    
    protected async Task<Response> SimpleDeleteAsync(string url = "")
    {
        await AddJwtTokenAsync();
        var response = await _httpClient.DeleteAsync(url);
        
        if (!response.IsSuccessStatusCode)
            return await GenerateFailedResponseAsync(response.Content);

        return new Response();
    }
}