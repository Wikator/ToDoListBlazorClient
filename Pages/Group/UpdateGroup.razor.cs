using AutoMapper;
using Microsoft.AspNetCore.Components;
using ToDoListBlazorClient.Models.DTOs.Group;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages.Group;

public partial class UpdateGroup
{
    [Parameter] public int? Id { get; init; }

    [Inject] public required IGroupService GroupService { private get; init; }
    [Inject] public required NavigationManager NavigationManager { private get; init; }
    [Inject] public required IMapper Mapper { private get; init; }

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
            var response = await GroupService.SimpleGetAsync(Id.Value);

            if (response.Data is null)
                Group = Mapper.Map<CreateGroupDto>(response.Data);
            else
                GetErrorMessage = response.Message;
        }
    }

    protected async Task HandleSubmit()
    {
        if (Id is null || Group is null)
            return;

        var response = await GroupService.SimplePutAsync(Id.Value, Group);
        if (response.IsSuccess)
            NavigationManager.NavigateTo("groups");
        else
            PutErrorMessage = response.Message;
    }
}