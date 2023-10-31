using System.ComponentModel.DataAnnotations;

namespace ToDoListBlazorClient.Models.DTOs;

public class CreateSubjectDto
{
    [Required] public string Name { get; set; } = "New Subject";
}