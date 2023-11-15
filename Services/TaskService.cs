using Blazored.LocalStorage;
using ToDoListBlazorClient.Extensions;
using ToDoListBlazorClient.Models.DTOs.Task;
using ToDoListBlazorClient.Services.Base;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services;

public class TaskService(ILocalStorageService localStorage,
        HttpClient httpClient)
    : SimpleHttpService<TaskDto, CreateTaskDto>(localStorage, httpClient), ITaskService
{
    private HttpClient HttpClient { get; } = httpClient;
    private ILocalStorageService LocalStorage { get; } = localStorage;
    
    
    protected override string BaseUrl => "tasks/";
    
    public async Task<Response<IEnumerable<TaskDto>>> MyTasks()
    {
        await HttpClient.AddJwtTokenAsync(LocalStorage);
        var response = await HttpClient.GetAsync("my_tasks");
        return await GenerateResponseAsync<IEnumerable<TaskDto>>(response);
    }
}