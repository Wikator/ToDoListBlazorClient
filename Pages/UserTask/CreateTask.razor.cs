using Microsoft.AspNetCore.Components;
using ToDoListBlazorClient.Models;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages.UserTask;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class CreateTask
{
    [Inject] public required IGroupService GroupService { private get; init; }
    [Inject] public required ISubjectService SubjectService { private get; init; }
    [Inject] public required ICategoryService CategoryService { private get; init; }


    private IEnumerable<Option>? GroupOptions { get; set; }
    private IEnumerable<Option>? SubjectOptions { get; set; }
    private IEnumerable<Option>? CategoryOptions { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var groups = await GroupService.SimpleGetAsync();
        var subjects = await SubjectService.SimpleGetAsync();
        var categories = await CategoryService.SimpleGetAsync();

        GroupOptions = groups.Data?.Select(g => new Option
        {
            Name = g.Name,
            Value = g.Id
        });

        SubjectOptions = subjects.Data?.Select(s => new Option
        {
            Name = s.Name,
            Value = s.Id
        });

        CategoryOptions = categories.Data?.Select(c => new Option
        {
            Name = c.Name,
            Value = c.Id
        });
    }
}