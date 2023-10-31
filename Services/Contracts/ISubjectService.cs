using ToDoListBlazorClient.Models.DTOs;

namespace ToDoListBlazorClient.Services.Contracts;

public interface ISubjectService
{
    Task<IEnumerable<SubjectDto>> GetSubjects();
    Task<SubjectDto> GetSubject(int id);
    Task<SubjectDto> CreateSubject(CreateSubjectDto subject);
    Task<SubjectDto> UpdateSubject(CreateSubjectDto subject);
    Task DeleteGroup(int id);
}