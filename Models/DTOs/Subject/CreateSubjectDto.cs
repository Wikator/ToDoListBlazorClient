using System.ComponentModel.DataAnnotations;

namespace ToDoListBlazorClient.Models.DTOs.Subject;

public class CreateSubjectDto
{
    [Required] public string Name { get; set; } = "New Subject";
    public IEnumerable<SubjectTimeAttributeDto>? SubjectTimesAttributes { get; set; }
}

public class SubjectTimeAttributeDto
{
    public int GroupId { get; init; }
    public TimeOnly? Time { get; set; }
}