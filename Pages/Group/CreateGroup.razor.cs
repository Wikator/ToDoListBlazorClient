﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using ToDoListBlazorClient.Models.DTOs.Group;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages.Group;

[Authorize(Roles = "admin")]
// ReSharper disable once ClassNeverInstantiated.Global
public partial class CreateGroup
{
    [Inject] public required IGroupService GroupService { get; init; }

    [Inject] public required NavigationManager NavigationManager { get; init; }

    private string? ErrorMessage { get; set; }

    private CreateGroupDto Group { get; } = new();

    protected async Task HandleSubmit()
    {
        var response = await GroupService.SimplePostAsync(Group);

        if (response.IsSuccess)
            NavigationManager.NavigateTo("/groups");
        else
            ErrorMessage = response.Message;
    }
}