using ToDoListBlazorClient.Models.DTOs.Group;

namespace ToDoListBlazorClient.Models.DTOs.SubjectTime;

public class SubjectTimeFromSubjectDto
{
    public int Id { get; set; }
    public required GroupDto Group { get; set; }
    public TimeOnly? Time { get; set; }
}