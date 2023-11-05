using Blazored.LocalStorage;
using ToDoListBlazorClient.Models.DTOs.Category;
using ToDoListBlazorClient.Services.Base;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services;

public sealed class CategoryService : SimpleHttpService<CategoryDto, CreateCategoryDto>, ICategoryService
{
    public CategoryService(ILocalStorageService localStorage,
        HttpClient httpClient) : base(localStorage, httpClient)
    {
    }

    protected override string BaseUrl => "categories/";
}