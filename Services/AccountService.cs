using System.Net;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using ToDoListBlazorClient.Extensions;
using ToDoListBlazorClient.Helpers;
using ToDoListBlazorClient.Models.DTOs.User;
using ToDoListBlazorClient.Providers;
using ToDoListBlazorClient.Services.Base;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services;

public class AccountService(HttpClient httpClient, ILocalStorageService localStorage,
        AuthenticationStateProvider authStateProvider)
    : IAccountService
{
    
    private HttpClient HttpClient { get; } = httpClient;
    private ILocalStorageService LocalStorage { get; } = localStorage;
    private ApiAuthenticationStateProvider AuthStateProvider { get; } =
        (ApiAuthenticationStateProvider)authStateProvider;

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

    public async Task<Response<UserDto>> ChangePasswordAsync(UserEditDto edit)
    {
        await HttpClient.AddJwtTokenAsync(LocalStorage);
        var stringContent = JsonUtility.GenerateSnakeCaseJson(edit);
        var response = await HttpClient.PutAsync("users", stringContent);

        if (!response.IsSuccessStatusCode)
            return await Response<UserDto>.GenerateFailedResponseAsync(response.Content);

        return await Response<UserDto>.GenerateSuccessfulResponseAsync(response.Content);
    }

    public async Task<Response> LogoutAsync()
    {
        await HttpClient.AddJwtTokenAsync(LocalStorage);
        var response = await HttpClient.DeleteAsync("users/sign_out");

        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode != HttpStatusCode.Unauthorized)
                return await Response.GenerateFailedResponseAsync(response.Content);

            await AuthStateProvider.LoggedOut();

            return await Response.GenerateFailedResponseAsync(response.Content);
        }

        HttpClient.DefaultRequestHeaders.Authorization = null;
        await AuthStateProvider.LoggedOut();
        return Response.GenerateSuccessfulResponse();
    }

    private async Task<Response<UserDto>> LoginOrRegister(string url, object body)
    {
        var stringContent = JsonUtility.GenerateSnakeCaseJson(body);
        var response = await HttpClient.PostAsync(url, stringContent);

        if (!response.IsSuccessStatusCode)
            return await Response<UserDto>.GenerateFailedResponseAsync(response.Content);

        var token = response.Headers.GetValues("Authorization").FirstOrDefault();

        if (token is null)
            return await Response<UserDto>.GenerateFailedResponseAsync(response.Content);

        var user = await Response<UserDto>.GenerateSuccessfulResponseAsync(response.Content);

        if (!user.IsSuccess)
            return user;

        token = token.Split(' ')[1].TrimEnd('"');
        await AuthStateProvider.LoggedIn(token);
        return user;
    }
}