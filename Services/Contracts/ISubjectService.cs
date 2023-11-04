using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Services.Base;

namespace ToDoListBlazorClient.Services.Contracts;

public interface ISubjectService
{
    Task<Response<IEnumerable<SubjectDto>>> GetSubjects();
    Task<Response<SubjectDto>> GetSubject(int id);
    Task<Response<SubjectDto>> CreateSubject(CreateSubjectDto subject);
    Task<Response<SubjectDto>> UpdateSubject(int id, CreateSubjectDto subject);
    Task<Response> DeleteSubject(int id);
}