namespace ToDoListBlazorClient.Models.DTOs;

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