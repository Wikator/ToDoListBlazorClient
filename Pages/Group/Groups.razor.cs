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

    private Response<IEnumerable<GroupDto>>? Response { get; set; }
    
    private List<string?> ErrorMessages { get; } = new();

    protected override async Task OnInitializedAsync()
    {
        Response = await GroupService.GetGroups();
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
        var response = await GroupService.DeleteGroup(groupId);
        
        if (!response.IsSuccess)
        {
            ErrorMessages.Add(response.Message);
        }
        else
        {
            if (Response is not null)
            {
                Response.Data = Response.Data?.Where(g => g.Id != groupId);
            }
        }
    }
}