using Microsoft.AspNetCore.Components;
using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages.Group;

public partial class UpdateGroup
{
    [Parameter] public int? Id { get; init; }

    [Inject] public required IGroupService GroupService { get; init; }

    [Inject] public required NavigationManager NavigationManager { get; init; }
    
    private string? GetErrorMessage { get; set; }
    private string? PutErrorMessage { get; set; }

    protected CreateGroupDto? Group { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (Id is null)
        {
            NavigationManager.NavigateTo("groups");
        }
        else
        {
            var response = await GroupService.GetGroup(Id.Value);
            
            if (!response.IsSuccess)
            {
                GetErrorMessage = response.Message;
                return;
            }

            Group = new CreateGroupDto
            {
                Name = response.Data!.Name
            };
        }
    }

    protected async Task HandleSubmit()
    {
        var response = await GroupService.UpdateGroup(Id!.Value, Group!);
        if (response.IsSuccess)
        {
            NavigationManager.NavigateTo("groups");
        }
        else
        {
            PutErrorMessage = response.Message;
        }
    }
}