using Microsoft.AspNetCore.Components;
using ToDoListBlazorClient.Models.DTOs.Task;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages.UserTask;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Tasks
{
    [Inject] public required ITaskService TaskService { private get; init; }
    [Inject] public required NavigationManager NavigationManager { private get; init; }

    private IEnumerable<TaskDto>? TaskList { get; set; }

    private string? GetErrorMessage { get; set; }
    private List<string> DeleteErrorMessages { get; } = new();

    protected override async Task OnInitializedAsync()
    {
        var response = await TaskService.SimpleGetAsync();

        if (response.IsSuccess)
        {
            TaskList = response.Data;
        }
        else
        {
            GetErrorMessage = response.Message;
        }
    }

    private void NavigateToCreateTask()
    {
        NavigationManager.NavigateTo("tasks/create");
    }

    private void NavigateToEditTask(int id)
    {
        NavigationManager.NavigateTo($"tasks/{id}");
    }

    private async Task DeleteTask(int id)
    {
        var response = await TaskService.SimpleDeleteAsync(id);

        if (response.IsSuccess)
        {
            TaskList = TaskList?.Where(t => t.Id != id);
        }
        else
        {
            DeleteErrorMessages.Add(response.Message);
        }
    }
}