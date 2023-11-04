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
   
   private string? GetErrorMessage { get; set; }
   
   private List<string?> DeleteErrorMessages { get; } = new();

   protected override async Task OnInitializedAsync()
   {
      var response = await SubjectService.SimpleGetAsync();

      if (!response.IsSuccess)
      {
         GetErrorMessage = response.Message;
      }
      else
      {
         SubjectList = response.Data;
      }
   }

   private void NavigateToCreateSubject()
   {
      NavigationManager.NavigateTo("/subjects/create");
   }

   private void NavigateToUpdateSubject(int id)
   {
      NavigationManager.NavigateTo($"subjects/{id}");
   }
   
   private async Task DeleteSubject(int id)
   {
      var response = await SubjectService.SimpleDeleteAsync(id);
      
      if (!response.IsSuccess)
      {
         DeleteErrorMessages.Add(response.Message);
      }
      else
      {
         SubjectList = SubjectList?.Where(s => s.Id != id);
      }
   }
}