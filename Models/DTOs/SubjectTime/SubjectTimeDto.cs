using ToDoListBlazorClient.Models.DTOs.Group;
using ToDoListBlazorClient.Models.DTOs.Subject;

namespace ToDoListBlazorClient.Models.DTOs.SubjectTime;

public class SubjectTimeDto
{
    public int Id { get; set; }
    public required SubjectDto Subject { get; set; }
    public required GroupDto Group { get; set; }
    public TimeOnly? Time { get; set; }
}