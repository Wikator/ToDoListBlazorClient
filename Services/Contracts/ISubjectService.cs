using ToDoListBlazorClient.Models.DTOs.Subject;

namespace ToDoListBlazorClient.Services.Contracts;

public interface ISubjectService : ISimpleHttpService<SubjectDto, CreateSubjectDto>
{
}