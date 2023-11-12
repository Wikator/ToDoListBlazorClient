using ToDoListBlazorClient.Models.DTOs.Subject;
using ToDoListBlazorClient.Models.DTOs.SubjectTime;
using ToDoListBlazorClient.Services.Base;

namespace ToDoListBlazorClient.Services.Contracts;

public interface ISubjectService : ISimpleHttpService<SubjectDto, CreateSubjectDto>
{
    Task<Response<IEnumerable<SubjectTimeFromSubjectDto>>> GetSubjectTimesAsync(int subjectId);
}