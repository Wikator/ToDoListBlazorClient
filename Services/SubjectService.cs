using Blazored.LocalStorage;
using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Services.Base;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services;

public class SubjectService : BaseHttpService, ISubjectService
{
    private const string BaseUrl = "subjects/";
    
    public SubjectService(ILocalStorageService localStorage,
        HttpClient http) : base(localStorage, http)
    {
    }

    public async Task<Response<IEnumerable<SubjectDto>>> GetSubjects()
    {
        return await SimpleGetAsync<IEnumerable<SubjectDto>>(BaseUrl);
    }

    public async Task<Response<SubjectDto>> GetSubject(int id)
    {
        return await SimpleGetAsync<SubjectDto>(BaseUrl + id);
    }

    public async Task<Response<SubjectDto>> CreateSubject(CreateSubjectDto subject)
    {
        return await SimplePostAsync<SubjectDto, CreateSubjectDto>(subject, BaseUrl);
    }

    public async Task<Response<SubjectDto>> UpdateSubject(int id, CreateSubjectDto subject)
    {
        return await SimplePutAsync<SubjectDto, CreateSubjectDto>(subject, BaseUrl + id);
    }

    public async Task<Response> DeleteSubject(int id)
    {
        return await SimpleDeleteAsync(BaseUrl + id);
    }
}