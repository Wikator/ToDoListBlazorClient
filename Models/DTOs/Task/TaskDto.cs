using ToDoListBlazorClient.Models.DTOs.Category;
using ToDoListBlazorClient.Models.DTOs.Group;
using ToDoListBlazorClient.Models.DTOs.Subject;

namespace ToDoListBlazorClient.Models.DTOs.Task;

// ReSharper disable once ClassNeverInstantiated.Global
public class TaskDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public DateTime? Deadline { get; init; }
    public required CategoryDto Category { get; init; }
    public SubjectDto? Subject { get; init; }
    public GroupDto? Group { get; init; }
}