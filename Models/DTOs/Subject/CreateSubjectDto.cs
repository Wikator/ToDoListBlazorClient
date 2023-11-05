using System.ComponentModel.DataAnnotations;

namespace ToDoListBlazorClient.Models.DTOs.Subject;

public class CreateSubjectDto
{
    [Required] public string Name { get; set; } = "New Subject";
}