using Microsoft.AspNetCore.Components;
using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Groups
{
    [Inject] public required IGroupService GroupService { get; init; }

    [Inject] public required NavigationManager NavigationManager { get; init; }

    private IEnumerable<GroupDto>? GroupList { get; set; }

    protected override async Task OnInitializedAsync()
    {
        GroupList = await GroupService.GetGroups();
    }

    private void NavigateToCreateGroup()
    {
        NavigationManager.NavigateTo("groups/create");
    }

    private void NavigateToUpdateGroup(int groupId)
    {
        NavigationManager.NavigateTo($"groups/{groupId}");
    }
}