using System.Text;
using System.Text.Json;
using Blazored.LocalStorage;
using ToDoListBlazorClient.Extensions;
using ToDoListBlazorClient.Helpers;
using ToDoListBlazorClient.Models.DTOs.User;
using ToDoListBlazorClient.Services.Base;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services;

public class RoleService : IRoleService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public RoleService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task<Response<IEnumerable<UserDto>>> GetUsers()
    {
        await _httpClient.AddJwtTokenAsync(_localStorage);
        var response = await _httpClient.GetAsync("users");

        if (!response.IsSuccessStatusCode)
            return await Response<IEnumerable<UserDto>>.GenerateFailedResponseAsync(response.Content);

        return await Response<IEnumerable<UserDto>>.GenerateSuccessfulResponseAsync(response.Content);
    }

    public async Task<Response> UpdateRoleAsync(int userId, string role)
    {
        await _httpClient.AddJwtTokenAsync(_localStorage);
        var response = await _httpClient.PutAsync($"roles/update_role/{userId}",
            GenerateSnakeCaseJson(new { role_name = role }));

        if (!response.IsSuccessStatusCode)
            return await Response.GenerateFailedResponseAsync(response.Content);

        return Response.GenerateSuccessfulResponse();
    }

    private static StringContent GenerateSnakeCaseJson<T>(T body)
    {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance
        };

        var requestBody = JsonSerializer.Serialize(body, jsonSerializerOptions);
        return new StringContent(requestBody, Encoding.UTF8, "application/json");
    }
}