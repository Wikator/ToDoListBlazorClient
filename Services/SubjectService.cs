using Blazored.LocalStorage;
using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Services.Base;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services;

public sealed class SubjectService : SimpleHttpService<SubjectDto, CreateSubjectDto>, ISubjectService
{
    public SubjectService(ILocalStorageService localStorage,
        HttpClient httpClient) : base(localStorage, httpClient)
    {
    }

    protected override string BaseUrl => "subjects/";
}