using Microsoft.AspNetCore.Components;
using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages.Subject;

public partial class Subjects
{ 
   [Inject]
   public required ISubjectService SubjectService { get; init; }
   
   [Inject]
   public required NavigationManager NavigationManager { get; init; }
    
   private IEnumerable<SubjectDto>? SubjectList { get; set; }

   protected override async Task OnInitializedAsync()
   {
      SubjectList = await SubjectService.GetSubjects();
   }

   private void NavigateToCreateSubject()
   {
      NavigationManager.NavigateTo("/subjects/create");
   }

   private void NavigateToUpdateSubject(int id)
   {
      NavigationManager.NavigateTo($"subject/{id}");
   }
}