using Blazored.LocalStorage;
using ToDoListBlazorClient.Models.DTOs.Task;
using ToDoListBlazorClient.Services.Base;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services;

public class TaskService : SimpleHttpService<TaskDto, CreateTaskDto>, ITaskService
{
    public TaskService(ILocalStorageService localStorage,
        HttpClient httpClient) : base(localStorage, httpClient)
    {
    }

    protected override string BaseUrl => "tasks/";
}