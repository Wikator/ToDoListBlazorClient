using Microsoft.AspNetCore.Components;
using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages.Category;

public partial class UpdateCategory
{
    [Parameter] public int? Id { get; set; }
    [Inject] public required ICategoryService CategoryService { private get; init; }
    [Inject] public required NavigationManager NavigationManager { private get; init; }
    
    private CreateCategoryDto? CategoryDto { get; set; }
    
    private string? GetErrorMessage { get; set; }
    private string? PutErrorMessage { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (Id is null)
        {
            NavigationManager.NavigateTo("categories");
            return;
        }
        
        var response = await CategoryService.SimpleGetAsync(Id.Value);

        if (response.IsSuccess)
        {
            CategoryDto = new CreateCategoryDto
            {
                Name = response.Data!.Name
            };
        }
        else
        {
            GetErrorMessage = response.Message;
        }
    }

    private async Task HandleSubmit()
    {
        if (Id is null || CategoryDto is null)
            return;
        
        var response = await CategoryService.SimplePutAsync(Id.Value, CategoryDto);
        
        if (response.IsSuccess)
        {
            NavigationManager.NavigateTo("categories");
        }
        else
        {
            PutErrorMessage = response.Message;
        }
    }
}