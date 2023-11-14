using Blazored.LocalStorage;
using ToDoListBlazorClient.Models.DTOs.Task;
using ToDoListBlazorClient.Services.Base;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services;

public class TaskService(ILocalStorageService localStorage,
        HttpClient httpClient)
    : SimpleHttpService<TaskDto, CreateTaskDto>(localStorage, httpClient), ITaskService
{
    protected override string BaseUrl => "tasks/";
}