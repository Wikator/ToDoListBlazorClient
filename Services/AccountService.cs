using System.Net;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Providers;
using ToDoListBlazorClient.Services.Base;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services;

public class AccountService : BaseHttpService, IAccountService
{
    private readonly HttpClient _httpClient;
    private readonly ApiAuthenticationStateProvider _authStateProvider;

    public AccountService(HttpClient httpClient, ILocalStorageService localStorage,
        AuthenticationStateProvider authStateProvider) : base(localStorage, httpClient)
    {
        _httpClient = httpClient;
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
        await AddJwtTokenAsync();
        var response = await _httpClient.DeleteAsync("users/sign_out");

        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode != HttpStatusCode.Unauthorized)
                return await GenerateFailedResponseAsync(response.Content);
            
            await _authStateProvider.LoggedOut();

            return await GenerateFailedResponseAsync(response.Content);
        }

        _httpClient.DefaultRequestHeaders.Authorization = null;
        await _authStateProvider.LoggedOut();
        return new Response();
    }

    private async Task<Response<UserDto>> LoginOrRegister(string url, object body)
    {
        var response = await _httpClient.PostAsJsonAsync(url, body);

        if (!response.IsSuccessStatusCode)
            return await GenerateFailedResponseAsync<UserDto>(response.Content);
        
        var token = response.Headers.GetValues("Authorization").FirstOrDefault();
        
        if (token is null)
            return await GenerateFailedResponseAsync<UserDto>(null);
        
        var user = await GenerateSuccessfulResponseAsync<UserDto>(response.Content);

        if (!user.IsSuccess)
            return user;
        
        token = token.Split(' ')[1].TrimEnd('"');
        await _authStateProvider.LoggedIn(token);
        return user;
    }
}