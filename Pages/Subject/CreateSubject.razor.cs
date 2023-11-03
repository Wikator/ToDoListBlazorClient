using Microsoft.AspNetCore.Components;
using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages.Subject;

public partial class CreateSubject
{
    [Inject]
    public required ISubjectService SubjectService { get; init; }
    
    [Inject]
    public required NavigationManager NavigationManager { get; init; }
    
    private CreateSubjectDto Subject { get; } = new();

    private async Task HandleSubmit()
    {
        await SubjectService.CreateSubject(Subject);
        NavigationManager.NavigateTo("subjects");
    }
}