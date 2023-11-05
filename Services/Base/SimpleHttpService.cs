using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Blazored.LocalStorage;
using ToDoListBlazorClient.Extensions;
using ToDoListBlazorClient.Helpers;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services.Base;

public abstract class SimpleHttpService<TResponse, TBody> : ISimpleHttpService<TResponse, TBody>
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    protected SimpleHttpService(ILocalStorageService localStorage,
        HttpClient httpClient)
    {
        _localStorage = localStorage;
        _httpClient = httpClient;
    }

    protected abstract string BaseUrl { get; }

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
        var response = await _httpClient.PostAsync(BaseUrl, GenerateSnakeCaseJson(body));
        return await GenerateResponseAsync<TResponse>(response);
    }

    public async Task<Response<TResponse>> SimplePutAsync(int id, TBody body)
    {
        await _httpClient.AddJwtTokenAsync(_localStorage);
        var response = await _httpClient.PutAsync(BaseUrl + id, GenerateSnakeCaseJson(body));
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

    private static StringContent GenerateSnakeCaseJson(TBody body)
    {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance
        };
        
        var requestBody = JsonSerializer.Serialize(body, jsonSerializerOptions);
        return new StringContent(requestBody, Encoding.UTF8, "application/json");
    }
}