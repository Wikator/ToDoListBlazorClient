using ToDoListBlazorClient.Models.DTOs.Group;

namespace ToDoListBlazorClient.Models.DTOs.SubjectTime;

public class SubjectTimeFromSubjectDto
{
    public int Id { get; init; }
    public required GroupDto Group { get; init; }
    public TimeOnly? Time { get; init; }
}