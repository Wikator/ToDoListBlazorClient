using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using ToDoListBlazorClient.Models.DTOs.Subject;
using ToDoListBlazorClient.Models.DTOs.SubjectTime;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages.Subject;

public partial class UpdateSubject
{
    private class UpdateSubjectModel
    {
        [Required] public required string Name { get; set; }
        public required List<(string, UpdateSubjectTimeModel)> SubjectTimes { get; init; }
    }

    private class UpdateSubjectTimeModel
    {
        public int Id { get; init; }
        public TimeOnly? Time { get; set; }
    }

    private UpdateSubjectModel? Subject { get; set; }

    [Parameter] public int? Id { get; set; }

    [Inject] public required ISubjectService SubjectService { get; init; }
    [Inject] public required ISubjectTimeService SubjectTimeService { get; init; }
    [Inject] public required NavigationManager NavigationManager { get; init; }

    private string? GetErrorMessage { get; set; }
    private string? PutErrorMessage { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (Id is null)
        {
            NavigationManager.NavigateTo("/subjects");
        }
        else
        {
            var subjectTask = SubjectService.SimpleGetAsync(Id.Value);
            var subjectTimesTask = SubjectService.GetSubjectTimesAsync(Id.Value);

            await Task.WhenAll(subjectTask, subjectTimesTask);

            var subject = subjectTask.Result.Data;
            var subjectTimes = subjectTimesTask.Result.Data;

            if (subject is null || subjectTimes is null)
            {
                GetErrorMessage = "Failed to load data. Please try again later.";
                return;
            }

            Subject = new UpdateSubjectModel
            {
                Name = subject.Name,
                SubjectTimes = subjectTimes.Select(s => (s.Group.Name, new UpdateSubjectTimeModel
                {
                    Id = s.Id,
                    Time = s.Time
                })).ToList()
            };
        }
    }

    private async Task HandleSubmit()
    {
        if (Subject is null || Id is null)
            return;

        var subjectDto = new CreateSubjectDto()
        {
            Name = Subject.Name
        };

        var subjectTask = SubjectService.SimplePutAsync(Id.Value, subjectDto);
        var subjectTimesTasks = Subject.SubjectTimes.Select(s =>
            SubjectTimeService.UpdateSubjectTimeAsync(s.Item2.Id, new UpdateSubjectTimeDto
            {
                Time = s.Item2.Time
            })).ToList();

        var allTasks = new List<Task> { subjectTask };
        allTasks.AddRange(subjectTimesTasks);

        await Task.WhenAll(allTasks);

        if (!subjectTask.Result.IsSuccess || !subjectTimesTasks.All(s => s.Result.IsSuccess))
            PutErrorMessage = "Something went wrong. Please try again later.";
        else
            NavigationManager.NavigateTo("/subjects");
    }
}