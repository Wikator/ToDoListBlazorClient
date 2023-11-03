using Microsoft.AspNetCore.Components;
using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages.Group;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class CreateGroup
{
    [Inject] public required IGroupService GroupService { get; init; }

    [Inject] public required NavigationManager NavigationManager { get; init; }
    
    private string? ErrorMessage { get; set; }

    private CreateGroupDto Group { get; } = new();

    protected async Task HandleSubmit()
    {
        var response = await GroupService.CreateGroup(Group);

        if (response.IsSuccess)
        {
            NavigationManager.NavigateTo("/groups");
        }
        else
        {
            ErrorMessage = response.Message;
        }
    }
}