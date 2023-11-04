using Microsoft.AspNetCore.Components;
using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Services.Base;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages.Group;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Groups
{
    [Inject] public required IGroupService GroupService { get; init; }

    [Inject] public required NavigationManager NavigationManager { get; init; }

    private IEnumerable<GroupDto>? GroupList { get; set; }

    
    private string? GetErrorMessage { get; set; }
    private List<string?> DeleteErrorMessages { get; } = new();

    protected override async Task OnInitializedAsync()
    {
        var response = await GroupService.SimpleGetAsync();
        
        if (!response.IsSuccess)
        {
            GetErrorMessage = response.Message;
        }
        else
        {
            GroupList = response.Data;
        }
    }

    private void NavigateToCreateGroup()
    {
        NavigationManager.NavigateTo("groups/create");
    }

    private void NavigateToUpdateGroup(int groupId)
    {
        NavigationManager.NavigateTo($"groups/{groupId}");
    }
    
    private async Task DeleteGroup(int groupId)
    {
        var response = await GroupService.SimpleDeleteAsync(groupId);
        
        if (!response.IsSuccess)
        {
            DeleteErrorMessages.Add(response.Message);
        }
        else
        {
            GroupList = GroupList?.Where(g => g.Id != groupId);
        }
    }
}