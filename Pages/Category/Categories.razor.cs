using Microsoft.AspNetCore.Components;
using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages.Category;

public partial class Categories
{
    [Inject]
    public required ICategoryService CategoryService { get; init; }
   
    [Inject]
    public required NavigationManager NavigationManager { get; init; }
    
    private IEnumerable<CategoryDto>? CategoryList { get; set; }
   
    private string? GetErrorMessage { get; set; }
   
    private List<string?> DeleteErrorMessages { get; } = new();

    protected override async Task OnInitializedAsync()
    {
        var response = await CategoryService.SimpleGetAsync();

        if (!response.IsSuccess)
        {
            GetErrorMessage = response.Message;
        }
        else
        {
            CategoryList = response.Data;
        }
    }

    private void NavigateToCreateCategory()
    {
        NavigationManager.NavigateTo("/categories/create");
    }

    private void NavigateToUpdateCategory(int id)
    {
        NavigationManager.NavigateTo($"categories/{id}");
    }
   
    private async Task DeleteCategory(int id)
    {
        var response = await CategoryService.SimpleDeleteAsync(id);
      
        if (!response.IsSuccess)
        {
            DeleteErrorMessages.Add(response.Message);
        }
        else
        {
            CategoryList = CategoryList?.Where(s => s.Id != id);
        }
    }
}