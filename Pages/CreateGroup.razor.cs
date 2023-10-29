using Microsoft.AspNetCore.Components;
using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages;

public partial class CreateGroup : ComponentBase
{
    [Inject] public required IGroupService GroupService { get; init; }

    [Inject] public required NavigationManager NavigationManager { get; init; }

    protected CreateGroupDto Group { get; } = new();

    protected async Task HandleSubmit()
    {
        await GroupService.CreateGroup(Group);
        NavigationManager.NavigateTo("groups");
    }
}