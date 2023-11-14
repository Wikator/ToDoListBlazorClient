using Blazored.LocalStorage;
using ToDoListBlazorClient.Models.DTOs.Category;
using ToDoListBlazorClient.Services.Base;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services;

public sealed class CategoryService(ILocalStorageService localStorage,
        HttpClient httpClient)
    : SimpleHttpService<CategoryDto, CreateCategoryDto>(localStorage, httpClient), ICategoryService
{
    protected override string BaseUrl => "categories/";
}