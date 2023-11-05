using ToDoListBlazorClient.Models.DTOs.Category;
using ToDoListBlazorClient.Models.DTOs.Group;
using ToDoListBlazorClient.Models.DTOs.Subject;

namespace ToDoListBlazorClient.Models.DTOs.Task;

public class TaskDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime? Deadline { get; set; }
    public required CategoryDto Category { get; set; }
    public SubjectDto? Subject { get; set; }
    public GroupDto? Group { get; set; }
}