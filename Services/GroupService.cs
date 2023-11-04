using Blazored.LocalStorage;
using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Services.Base;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services;

public class GroupService : BaseHttpService, IGroupService
{
    private const string BaseUrl = "groups";
    
    public GroupService(HttpClient httpClient,
        ILocalStorageService localStorage) : base(localStorage, httpClient)
    {
    }

    public async Task<Response<IEnumerable<GroupDto>>> GetGroups()
    {
        return await SimpleGetAsync<IEnumerable<GroupDto>>(BaseUrl);
    }

    public async Task<Response<GroupDto>> GetGroup(int id)
    {
        return await SimpleGetAsync<GroupDto>(BaseUrl + id);
    }

    public async Task<Response<GroupDto>> CreateGroup(CreateGroupDto group)
    {
        return await SimplePostAsync<GroupDto, CreateGroupDto>(group, BaseUrl);
    }

    public async Task<Response<GroupDto>> UpdateGroup(int id, CreateGroupDto group)
    {
        return await SimplePutAsync<GroupDto, CreateGroupDto>(group, BaseUrl + id);
    }

    public async Task<Response> DeleteGroup(int id)
    {
        return await SimpleDeleteAsync(BaseUrl + id);
    }
}