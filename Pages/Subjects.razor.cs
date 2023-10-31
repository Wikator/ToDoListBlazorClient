using Microsoft.AspNetCore.Components;
using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Pages;

public partial class Subjects
{ 
   [Inject]
   public required ISubjectService SubjectService { get; init; }
   
   [Inject]
   public required NavigationManager NavigationManager { get; init; }
    
   protected IEnumerable<SubjectDto>? SubjectList { get; private set; }

   protected override async Task OnInitializedAsync()
   {
      SubjectList = await SubjectService.GetSubjects();
   }

   private void NavigateToCreateSubject()
   {
      throw new NotImplementedException();
   }

   private void NavigateToUpdateSubject(int id)
   {
      throw new NotImplementedException();
   }
}