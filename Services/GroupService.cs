using Blazored.LocalStorage;
using ToDoListBlazorClient.Models.DTOs.Group;
using ToDoListBlazorClient.Services.Base;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services;

public sealed class GroupService : SimpleHttpService<GroupDto, CreateGroupDto>, IGroupService
{
    public GroupService(ILocalStorageService localStorage,
        HttpClient httpClient) : base(localStorage, httpClient)
    {
    }

    protected override string BaseUrl => "groups/";
}