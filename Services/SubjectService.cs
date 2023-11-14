using Blazored.LocalStorage;
using ToDoListBlazorClient.Extensions;
using ToDoListBlazorClient.Models.DTOs.Subject;
using ToDoListBlazorClient.Models.DTOs.SubjectTime;
using ToDoListBlazorClient.Services.Base;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services;

public sealed class SubjectService(ILocalStorageService localStorage,
        HttpClient httpClient)
    : SimpleHttpService<SubjectDto, CreateSubjectDto>(localStorage, httpClient), ISubjectService
{
    private ILocalStorageService LocalStorage { get; } = localStorage;
    private HttpClient HttpClient { get; } = httpClient;
    
    protected override string BaseUrl => "subjects/";

    public async Task<Response<IEnumerable<SubjectTimeFromSubjectDto>>> GetSubjectTimesAsync(int subjectId)
    {
        await HttpClient.AddJwtTokenAsync(LocalStorage);
        var response = await HttpClient.GetAsync($"{BaseUrl}{subjectId}/subject_times");

        if (!response.IsSuccessStatusCode)
            return await Response<IEnumerable<SubjectTimeFromSubjectDto>>.GenerateFailedResponseAsync(response.Content);

        return await Response<IEnumerable<SubjectTimeFromSubjectDto>>.GenerateSuccessfulResponseAsync(response.Content);
    }
}