using AutoMapper;
using Microsoft.AspNetCore.Components;
using ToDoListBlazorClient.Models;
using ToDoListBlazorClient.Models.DTOs.Task;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages.UserTask;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class UpdateTask
{
    [Parameter] public int? Id { get; init; }
    
    [Inject] public required ITaskService TaskService { get; init; }
    [Inject] public required IGroupService GroupService { get; init; }
    [Inject] public required ISubjectService SubjectService { get; init; }
    [Inject] public required ICategoryService CategoryService { get; init; }
    [Inject] public required NavigationManager NavigationManager { get; init; }
    [Inject] public required IMapper Mapper { get; init; }

    private Dictionary<string, IEnumerable<Option>>? Options { get; set; }
    private CreateTaskDto? Model { get; set; }
    
    private string? GetErrorMessage { get; set; }
    private string? FetchErrorMessage { get; set; }
    private string? UpdateErrorMessage { get; set; }


    protected override async Task OnInitializedAsync()
    {
        var groupsTask = GroupService.SimpleGetAsync();
        var subjectsTask = SubjectService.SimpleGetAsync();
        var categoriesTask = CategoryService.SimpleGetAsync();

        await Task.WhenAll(groupsTask, subjectsTask, categoriesTask);
        
        var groupsResponse = groupsTask.Result;
        var subjectsResponse = subjectsTask.Result;
        var categoriesResponse = categoriesTask.Result;
        
        if (groupsResponse.Data is null || subjectsResponse.Data is null || categoriesResponse.Data is null)
        {
            GetErrorMessage = "Failed to load data. Please try again later.";
            return;
        }

        Options = new Dictionary<string, IEnumerable<Option>>
        {
            { "Group", Mapper.Map<IEnumerable<Option>>(groupsResponse.Data) },
            { "Subject", Mapper.Map<IEnumerable<Option>>(subjectsResponse.Data) },
            { "Categories", Mapper.Map<IEnumerable<Option>>(categoriesResponse.Data) }
        };
    }

    protected override async Task OnParametersSetAsync()
    {
        if (Id is null)
        {
            NavigationManager.NavigateTo("/tasks");
        }
        else
        {
            var response = await TaskService.SimpleGetAsync(Id.Value);

            if (response.Data is null)
            {
                FetchErrorMessage = response.Message ??"Something went wrong fetching data";
            }
            else
            {
                Model = Mapper.Map<CreateTaskDto>(response.Data);
            }
        }
    }

    private async Task HandleSubmit()
    {
        if (Id is null || Model is null)
            return;
        
        var response = await TaskService.SimplePutAsync(Id.Value, Model);

        if (response.IsSuccess)
        {
            NavigationManager.NavigateTo("/tasks");
        }
        else
        {
            UpdateErrorMessage = response.Message ?? "Something went wrong when updating. Try again later";
        }
    }
}