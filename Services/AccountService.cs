using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services;

public class AccountService : IAccountService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public AccountService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public UserDto? CurrentUser { get; private set; }


    public async Task<UserDto?> SignInAsync(LoginDto login)
    {
        object body = new
        {
            user = login
        };

        using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("users/sign_in", body);
        return await GetUser(response);
    }

    public async Task<UserDto?> RegisterAsync(RegisterDto register)
    {
        object body = new
        {
            user = register
        };

        using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("users", body);
        return await GetUser(response);
    }

    public async Task LogoutAsync()
    {
        var response = await _httpClient.DeleteAsync("users");

        if (response.IsSuccessStatusCode)
        {
            await _localStorage.RemoveItemAsync("user");
        }
    }

    public async Task SetUserAsync()
    {
        UserDto? user = await _localStorage.GetItemAsync<UserDto>("user");

        if (user is not null)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", user.Token);
        }
        
        CurrentUser = user;
    }

    private async Task<UserDto> GetUser(HttpResponseMessage response)
    {
        UserDto? user = await response.Content.ReadFromJsonAsync<UserDto>();
        string? token = response.Headers.GetValues("Authorization").FirstOrDefault();
        if (user is null || token is null) throw new Exception("Invalid response");
        user.Token = token.Split(' ')[1].TrimEnd('"');
        await _localStorage.SetItemAsync("user", user);
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token.Split(' ')[1].TrimEnd('"'));
        return user;
    }
}