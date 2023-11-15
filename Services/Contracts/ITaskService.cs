using ToDoListBlazorClient.Models.DTOs.Task;
using ToDoListBlazorClient.Services.Base;

namespace ToDoListBlazorClient.Services.Contracts;

public interface ITaskService : ISimpleHttpService<TaskDto, CreateTaskDto>
{
    Task<Response<IEnumerable<TaskDto>>> MyTasks();
}