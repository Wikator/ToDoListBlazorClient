using System.ComponentModel.DataAnnotations;

namespace ToDoListBlazorClient.Models.DTOs.Task;

public class CreateTaskDto
{
    [Required] public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime? Deadline { get; set; }

    [Required] public int? CategoryId { get; set; }

    public int? SubjectId { get; set; }
    public int? GroupId { get; set; }
}