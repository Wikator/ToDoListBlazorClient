using ToDoListBlazorClient.Models.DTOs.Category;

namespace ToDoListBlazorClient.Services.Contracts;

public interface ICategoryService : ISimpleHttpService<CategoryDto, CreateCategoryDto>;