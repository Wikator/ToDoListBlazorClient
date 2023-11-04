using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Services.Base;

namespace ToDoListBlazorClient.Services.Contracts;

public interface ISubjectService : ISimpleHttpService<SubjectDto, CreateSubjectDto>
{
}