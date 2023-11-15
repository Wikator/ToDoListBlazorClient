using Blazored.LocalStorage;
using ToDoListBlazorClient.Extensions;
using ToDoListBlazorClient.Helpers;
using ToDoListBlazorClient.Models;
using ToDoListBlazorClient.Models.DTOs.User;
using ToDoListBlazorClient.Services.Base;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services;

public class RoleService(HttpClient httpClient, ILocalStorageService localStorage) : IRoleService
{
    private HttpClient HttpClient { get; } = httpClient;
    private ILocalStorageService LocalStorage { get; } = localStorage;
    
    
    public async Task<Response<IEnumerable<UserDto>>> GetUsers()
    {
        await HttpClient.AddJwtTokenAsync(LocalStorage);
        var response = await HttpClient.GetAsync("users");

        if (!response.IsSuccessStatusCode)
            return await Response<IEnumerable<UserDto>>.GenerateFailedResponseAsync(response.Content);

        return await Response<IEnumerable<UserDto>>.GenerateSuccessfulResponseAsync(response.Content);
    }

    public async Task<Response> UpdateRoleAsync(int userId, string role)
    {
        await HttpClient.AddJwtTokenAsync(LocalStorage);
        var stringContent = JsonUtility.GenerateSnakeCaseJson(new { role_name = role });
        var response = await HttpClient.PutAsync($"roles/update_role/{userId}", stringContent);

        if (!response.IsSuccessStatusCode)
            return await Response.GenerateFailedResponseAsync(response.Content);

        return Response.GenerateSuccessfulResponse();
    }
}