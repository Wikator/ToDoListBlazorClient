using Blazored.LocalStorage;
using ToDoListBlazorClient.Models.DTOs.Group;
using ToDoListBlazorClient.Services.Base;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services;

public sealed class GroupService(ILocalStorageService localStorage,
        HttpClient httpClient)
    : SimpleHttpService<GroupDto, CreateGroupDto>(localStorage, httpClient), IGroupService
{
    protected override string BaseUrl => "groups/";
}