using System.Net.Http.Json;
using Blazored.LocalStorage;
using ToDoListBlazorClient.Extensions;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services.Base;

public abstract class SimpleHttpService<TResponse, TBody> : ISimpleHttpService<TResponse, TBody>
{
    protected abstract string BaseUrl { get; }
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    protected SimpleHttpService(ILocalStorageService localStorage,
        HttpClient httpClient)
    {
        _localStorage = localStorage;
        _httpClient = httpClient;
    }
    
    public async Task<Response<IEnumerable<TResponse>>> SimpleGetAsync()
    {
        await _httpClient.AddJwtTokenAsync(_localStorage);
        var response = await _httpClient.GetAsync(BaseUrl);
        
        return await GenerateResponseAsync<IEnumerable<TResponse>>(response);
    }
    
    public async Task<Response<TResponse>> SimpleGetAsync(int id)
    {
        await _httpClient.AddJwtTokenAsync(_localStorage);
        var response = await _httpClient.GetAsync(BaseUrl + id);
        
        return await GenerateResponseAsync<TResponse>(response);
    }
    
    public async Task<Response<TResponse>> SimplePostAsync(TBody body)
    {
        await _httpClient.AddJwtTokenAsync(_localStorage);
        var response = await _httpClient.PostAsJsonAsync(BaseUrl, body);
        
        return await GenerateResponseAsync<TResponse>(response);
    }
    
    public async Task<Response<TResponse>> SimplePutAsync(int id, TBody body)
    {
        await _httpClient.AddJwtTokenAsync(_localStorage);
        var response = await _httpClient.PutAsJsonAsync(BaseUrl + id, body);
        
        return await GenerateResponseAsync<TResponse>(response);
    }
    
    public async Task<Response> SimpleDeleteAsync(int id)
    {
        await _httpClient.AddJwtTokenAsync(_localStorage);
        var response = await _httpClient.DeleteAsync(BaseUrl + id);

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

        return new Response();
    }
}