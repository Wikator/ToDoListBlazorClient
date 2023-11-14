using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace ToDoListBlazorClient.Providers;

public class ApiAuthenticationStateProvider(ILocalStorageService localStorage) : AuthenticationStateProvider
{
    private ILocalStorageService LocalStorage { get; } = localStorage;
    private readonly JwtSecurityTokenHandler _tokenHandler = new();

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity());
        var token = await LocalStorage.GetItemAsync<string>("accessToken");

        if (token.IsNullOrEmpty())
        {
            await LocalStorage.RemoveItemAsync("accessToken");
            return new AuthenticationState(user);
        }

        var decodedToken = _tokenHandler.ReadJwtToken(token);

        if (decodedToken.ValidTo < DateTime.Now)
        {
            await LocalStorage.RemoveItemAsync("accessToken");
            return new AuthenticationState(user);
        }

        var claims = decodedToken.Claims;
        user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
        return new AuthenticationState(user);
    }

    public async Task LoggedIn(string token)
    {
        await LocalStorage.SetItemAsync("accessToken", token);
        var decodedToken = _tokenHandler.ReadJwtToken(token);
        var claims = decodedToken.Claims;
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
        var authState = Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authState);
    }

    public async Task LoggedOut()
    {
        await LocalStorage.RemoveItemAsync("accessToken");
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(anonymousUser));
        NotifyAuthenticationStateChanged(authState);
    }
}