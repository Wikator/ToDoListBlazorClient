using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using ToDoListBlazorClient.Models.DTOs.Subject;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages.Subject;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class CreateSubject
{
    private class CreateSubjectModel
    {
        [Required] public string Name { get; set; } = "New Subject";

        public List<(string, SubjectTimeAttributeDto)>? SubjectTimesAttributes { get; set; }
    }

    private CreateSubjectModel Subject { get; } = new();

    [Inject] public required ISubjectService SubjectService { get; init; }
    [Inject] public required IGroupService GroupService { get; init; }
    [Inject] public required NavigationManager NavigationManager { get; init; }

    private string? FetchErrorMessage { get; set; }
    private string? CreateErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var response = await GroupService.SimpleGetAsync();

        if (response.Data is null)
            FetchErrorMessage = response.Message;
        else
            Subject.SubjectTimesAttributes = response.Data.Select(g => (g.Name, new SubjectTimeAttributeDto
            {
                GroupId = g.Id,
                Time = null
            })).ToList();
    }

    private async Task HandleSubmit()
    {
        var createSubjectDto = new CreateSubjectDto
        {
            Name = Subject.Name,
            SubjectTimesAttributes = Subject.SubjectTimesAttributes?.Select(s => s.Item2)
        };

        var response = await SubjectService.SimplePostAsync(createSubjectDto);

        if (!response.IsSuccess)
            CreateErrorMessage = response.Message;
        else
            NavigationManager.NavigateTo("/subjects");
    }
}