using ToDoListBlazorClient.Models.DTOs;

namespace ToDoListBlazorClient.Services.Contracts;

public interface ICategoryService : ISimpleHttpService<CategoryDto, CreateCategoryDto>
{
}