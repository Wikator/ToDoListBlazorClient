using Microsoft.AspNetCore.Components;
using ToDoListBlazorClient.Models.DTOs.Category;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages.Category;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class CreateCategory
{
    [Inject] public required ICategoryService CategoryService { private get; init; }
    [Inject] public required NavigationManager NavigationManager { private get; init; }

    private CreateCategoryDto Category { get; } = new();

    private string? ErrorMessage { get; set; }

    private async Task HandleSubmit()
    {
        var response = await CategoryService.SimplePostAsync(Category);

        if (response.IsSuccess)
            NavigationManager.NavigateTo("/categories");
        else
            ErrorMessage = response.Message;
    }
}