using ToDoListBlazorClient.Models.DTOs.Task;

namespace ToDoListBlazorClient.Services.Contracts;

public interface ITaskService : ISimpleHttpService<TaskDto, CreateTaskDto>
{
}