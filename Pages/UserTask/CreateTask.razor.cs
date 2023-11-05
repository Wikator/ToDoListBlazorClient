using Microsoft.AspNetCore.Components;
using ToDoListBlazorClient.Models;
using ToDoListBlazorClient.Models.DTOs.Task;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages.UserTask;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class CreateTask
{
    [Inject] public required IGroupService GroupService { private get; init; }
    [Inject] public required ISubjectService SubjectService { private get; init; }
    [Inject] public required ICategoryService CategoryService { private get; init; }
    [Inject] public required ITaskService TaskService { private get; init; }
    [Inject] public required NavigationManager NavigationManager { private get; init; }
    
    private string? GetErrorMessage { get; set; }
    private string? PostErrorMessage { get; set; }

    private Dictionary<string, IEnumerable<Option>?>? Options { get; set; }
    
    private CreateTaskDto CreateTaskDto { get; } = new();

    protected override async Task OnInitializedAsync()
    {
        var groupsTask = GroupService.SimpleGetAsync();
        var subjectsTask = SubjectService.SimpleGetAsync();
        var categoriesTask = CategoryService.SimpleGetAsync();

        await Task.WhenAll(groupsTask, subjectsTask, categoriesTask);
        
        var groupsResponse = groupsTask.Result;
        var subjectsResponse = subjectsTask.Result;
        var categoriesResponse = categoriesTask.Result;
        
        if (!groupsResponse.IsSuccess || !subjectsResponse.IsSuccess || !categoriesResponse.IsSuccess)
        {
            GetErrorMessage = "Failed to load data. Please try again later.";
            return;
        }

        var groups = groupsResponse.Data?.Select(g => new Option
        {
            Name = g.Name,
            Value = g.Id
        });

        var subjects = subjectsResponse.Data?.Select(s => new Option
        {
            Name = s.Name,
            Value = s.Id
        });

        var categories = categoriesResponse.Data?.Select(c => new Option
        {
            Name = c.Name,
            Value = c.Id
        });

        Options = new Dictionary<string, IEnumerable<Option>?>
        {
            { "Group", groups },
            { "Subject", subjects },
            { "Categories", categories }
        };
    }
    
    private async Task HandleSubmit()
    {
        var response = await TaskService.SimplePostAsync(CreateTaskDto);
        if (!response.IsSuccess)
        {
            PostErrorMessage = response.Message;
        }
        else
        {
            NavigationManager.NavigateTo("/tasks");
        }
    }
}