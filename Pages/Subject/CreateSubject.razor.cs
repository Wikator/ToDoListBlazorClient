using Microsoft.AspNetCore.Components;
using ToDoListBlazorClient.Models.DTOs.Subject;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages.Subject;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class CreateSubject
{
    [Inject] public required ISubjectService SubjectService { get; init; }

    [Inject] public required NavigationManager NavigationManager { get; init; }

    private CreateSubjectDto Subject { get; } = new();

    private string? ErrorMessage { get; set; }

    private async Task HandleSubmit()
    {
        var response = await SubjectService.SimplePostAsync(Subject);

        if (!response.IsSuccess)
        {
            ErrorMessage = response.Message;
        }
        else
        {
            NavigationManager.NavigateTo("/subjects");
        }
    }
}