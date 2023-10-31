using Microsoft.AspNetCore.Components;
using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages;

public partial class UpdateGroup
{
    [Parameter] public int? Id { get; init; }

    [Inject] public required IGroupService GroupService { get; init; }

    [Inject] public required NavigationManager NavigationManager { get; init; }

    protected CreateGroupDto? Group { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (Id is null)
            NavigationManager.NavigateTo("groups");

        Group = new CreateGroupDto
        {
            Name = (await GroupService.GetGroup(Id!.Value)).Name
        };
    }

    protected async Task HandleSubmit()
    {
        await GroupService.UpdateGroup(Id!.Value, Group!);
        NavigationManager.NavigateTo("groups");
    }
}