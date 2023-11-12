using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ToDoListBlazorClient.Extensions;
using ToDoListBlazorClient.Models.DTOs.User;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages.UserManagment;

public partial class UpdateRole
{
    [Inject] public required IRoleService RoleService { get; init; }
    [Inject] public required NavigationManager NavigationManager { get; init; }
    [Inject] public required AuthenticationStateProvider AuthenticationStateProvider { get; init; }

    private int UserId { get; set; }

    private string? FetchErrorMessage { get; set; }
    private List<string> DeleteErrorMessages { get; } = new();
    private IEnumerable<UserDto>? Users { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var response = await RoleService.GetUsers();

        if (response.Data is null)
            FetchErrorMessage = response.Message;
        else
            Users = response.Data;

        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        UserId = user.GetId()!.Value;
    }

    private async Task UpdateUserRole(int id, string role)
    {
        var response = await RoleService.UpdateRoleAsync(id, role);

        if (!response.IsSuccess)
            DeleteErrorMessages.Add(response.Message);
        else
            NavigationManager.NavigateTo("/update_role", true);
    }
}