using System.ComponentModel.DataAnnotations;

namespace ToDoListBlazorClient.Models.DTOs.Category;

public class CreateCategoryDto
{
    [Required]
    public string Name { get; set; } = "New Category";
}