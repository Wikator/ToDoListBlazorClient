using System.Net;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services;

public class GroupService : IGroupService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public GroupService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task<IEnumerable<GroupDto>> GetGroups()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("groups");

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            Console.WriteLine("Failed to authorize");
            return Enumerable.Empty<GroupDto>();
        }

        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong");

        return await response.Content.ReadFromJsonAsync<IEnumerable<GroupDto>>()
               ?? throw new Exception("Something went wrong");
    }

    public async Task<GroupDto> GetGroup(int id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"groups/{id}");

        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong");

        return await response.Content.ReadFromJsonAsync<GroupDto>()
               ?? throw new Exception("Something went wrong");
    }

    public async Task<GroupDto> CreateGroup(CreateGroupDto group)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("groups", group);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong");

        return await response.Content.ReadFromJsonAsync<GroupDto>()
               ?? throw new Exception("Something went wrong");
    }

    public async Task<GroupDto> UpdateGroup(int id, CreateGroupDto group)
    {
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"groups/{id}", group);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong");

        return await response.Content.ReadFromJsonAsync<GroupDto>()
               ?? throw new Exception("Something went wrong");
    }

    public Task DeleteGroup(int id)
    {
        throw new NotImplementedException();
    }
}