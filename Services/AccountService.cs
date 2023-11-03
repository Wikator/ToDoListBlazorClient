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
    private readonly ILocalStorageService _localStorage;
    private readonly ApiAuthenticationStateProvider _authStateProvider;

    public AccountService(HttpClient httpClient, ILocalStorageService localStorage,
        AuthenticationStateProvider authStateProvider) : base(localStorage, httpClient)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _authStateProvider = (ApiAuthenticationStateProvider)authStateProvider;
    }

    public async Task<Response<UserDto>> SignInAsync(LoginDto login)
    {
        var response = new Response<UserDto>();
        
        object body = new
        {
            user = login
        };

        var responseFromApi = await _httpClient.PostAsJsonAsync("users/sign_in", body);

        if (!responseFromApi.IsSuccessStatusCode)
        {
            response.IsSuccess = false;
            response.Message = await responseFromApi.Content.ReadAsStringAsync();
            return response;
        }
        
        var user = await responseFromApi.Content.ReadFromJsonAsync<UserDto>();
        var token = responseFromApi.Headers.GetValues("Authorization").FirstOrDefault();
        
        if (user is null || token is null)
        {
            response.IsSuccess = false;
            response.Message = "Something went wrong";
            return response;
        }
        
        token = token.Split(' ')[1].TrimEnd('"');
        await _localStorage.SetItemAsync("accessToken", token);
        _authStateProvider.LoggedIn(token);
        response.Data = user;
        return response;
    }

    public async Task<Response<UserDto>> RegisterAsync(RegisterDto register)
    {
        var response = new Response<UserDto>();
        
        object body = new
        {
            user = register
        };

        var responseFromApi = await _httpClient.PostAsJsonAsync("users", body);
        
        if (!responseFromApi.IsSuccessStatusCode)
        {
            response.IsSuccess = false;
            response.Message = await responseFromApi.Content.ReadAsStringAsync();
            return response;
        }
        
        var user = await responseFromApi.Content.ReadFromJsonAsync<UserDto>();
        var token = responseFromApi.Headers.GetValues("Authorization").FirstOrDefault();
        
        if (user is null || token is null)
        {
            response.IsSuccess = false;
            response.Message = "Something went wrong";
            return response;
        }
        
        token = token.Split(' ')[1].TrimEnd('"');
        await _localStorage.SetItemAsync("accessToken", token);
        _authStateProvider.LoggedIn(token);
        response.Data = user;
        return response;
    }

    public async Task<Response> LogoutAsync()
    {
        await AddJwtTokenAsync();
        var response = new Response();
        var responseFromApi = await _httpClient.DeleteAsync("users/sign_out");

        if (!responseFromApi.IsSuccessStatusCode)
        {
            if (responseFromApi.StatusCode == HttpStatusCode.Unauthorized)
            {
                RemoveJwtToken();
                await _authStateProvider.LoggedOut();
            }
            response.IsSuccess = false;
            response.Message = await responseFromApi.Content.ReadAsStringAsync();
            return response;
        }

        RemoveJwtToken();
        await _authStateProvider.LoggedOut();
        return new Response();
    }
}