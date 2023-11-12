using AutoMapper;
using Microsoft.AspNetCore.Components;
using ToDoListBlazorClient.Models.DTOs.Category;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages.Category;

public partial class UpdateCategory
{
    [Parameter] public int? Id { get; set; }
    [Inject] public required ICategoryService CategoryService { private get; init; }
    [Inject] public required NavigationManager NavigationManager { private get; init; }
    [Inject] public required IMapper Mapper { private get; init; }

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

        if (response.Data is null)
            GetErrorMessage = response.Message;
        else
            CategoryDto = Mapper.Map<CreateCategoryDto>(response.Data);
    }

    private async Task HandleSubmit()
    {
        if (Id is null || CategoryDto is null)
            return;

        var response = await CategoryService.SimplePutAsync(Id.Value, CategoryDto);

        if (response.IsSuccess)
            NavigationManager.NavigateTo("categories");
        else
            PutErrorMessage = response.Message;
    }
}