using ToDoListBlazorClient.Models.DTOs.SubjectTime;
using ToDoListBlazorClient.Services.Base;

namespace ToDoListBlazorClient.Services.Contracts;

public interface ISubjectTimeService
{
    Task<Response<SubjectTimeDto>> UpdateSubjectTimeAsync(int id, UpdateSubjectTimeDto updateSubjectTimeDto);
}