using System.Net;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using ToDoListBlazorClient.Extensions;
using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Providers;
using ToDoListBlazorClient.Services.Base;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services;

public class AccountService : IAccountService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly ApiAuthenticationStateProvider _authStateProvider;

    public AccountService(HttpClient httpClient, ILocalStorageService localStorage,
        AuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _authStateProvider = (ApiAuthenticationStateProvider)authStateProvider;
    }

    public async Task<Response<UserDto>> SignInAsync(LoginDto login)
    {
        object body = new
        {
            user = login
        };

        return await LoginOrRegister("users/sign_in", body);
    }

    public async Task<Response<UserDto>> RegisterAsync(RegisterDto register)
    {
        object body = new
        {
            user = register
        };

        return await LoginOrRegister("users", body);
    }

    public async Task<Response> LogoutAsync()
    {
        await _httpClient.AddJwtTokenAsync(_localStorage);
        var response = await _httpClient.DeleteAsync("users/sign_out");

        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode != HttpStatusCode.Unauthorized)
                return await Response.GenerateFailedResponseAsync(response.Content);
            
            await _authStateProvider.LoggedOut();

            return await Response.GenerateFailedResponseAsync(response.Content);
        }

        _httpClient.DefaultRequestHeaders.Authorization = null;
        await _authStateProvider.LoggedOut();
        return new Response();
    }

    private async Task<Response<UserDto>> LoginOrRegister(string url, object body)
    {
        var response = await _httpClient.PostAsJsonAsync(url, body);

        if (!response.IsSuccessStatusCode)
            return await Response<UserDto>.GenerateFailedResponseAsync(response.Content);
        
        var token = response.Headers.GetValues("Authorization").FirstOrDefault();
        
        if (token is null)
            return await Response<UserDto>.GenerateFailedResponseAsync(null);
        
        var user = await Response<UserDto>.GenerateSuccessfulResponseAsync(response.Content);

        if (!user.IsSuccess)
            return user;
        
        token = token.Split(' ')[1].TrimEnd('"');
        await _authStateProvider.LoggedIn(token);
        return user;
    }
}