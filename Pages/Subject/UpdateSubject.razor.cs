using Microsoft.AspNetCore.Components;
using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages.Subject;

public partial class UpdateSubject
{
    [Parameter] public int? Id { get; set; }
    [Inject] public required ISubjectService SubjectService { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }
    
    private string? GetErrorMessage { get; set; }
    private string? PutErrorMessage { get; set; }
    
    private CreateSubjectDto? Subject { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (Id is null)
        {
            NavigationManager.NavigateTo("/subjects");
        }
        else
        {
            var response = await SubjectService.SimpleGetAsync(Id.Value);
            
            if (!response.IsSuccess)
            {
                GetErrorMessage = response.Message;
            }
            else
            {
                Subject = new CreateSubjectDto
                {
                    Name = response.Data!.Name
                };
            }
        }
    }

    private async Task HandleSubmit()
    {
        if (Subject is null || Id is null)
            return;
        
        var response = await SubjectService.SimplePutAsync(Id.Value, Subject);
        
        if (!response.IsSuccess)
        {
            PutErrorMessage = response.Message;
        }
        else
        {
            NavigationManager.NavigateTo("/subjects");
        }
    }
}