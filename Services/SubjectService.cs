using Blazored.LocalStorage;
using ToDoListBlazorClient.Extensions;
using ToDoListBlazorClient.Models.DTOs.Subject;
using ToDoListBlazorClient.Models.DTOs.SubjectTime;
using ToDoListBlazorClient.Services.Base;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services;

public sealed class SubjectService : SimpleHttpService<SubjectDto, CreateSubjectDto>, ISubjectService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public SubjectService(ILocalStorageService localStorage,
        HttpClient httpClient) : base(localStorage, httpClient)
    {
        _localStorage = localStorage;
        _httpClient = httpClient;
    }

    protected override string BaseUrl => "subjects/";

    public async Task<Response<IEnumerable<SubjectTimeFromSubjectDto>>> GetSubjectTimesAsync(int subjectId)
    {
        await _httpClient.AddJwtTokenAsync(_localStorage);
        var response = await _httpClient.GetAsync($"{BaseUrl}{subjectId}/subject_times");

        if (!response.IsSuccessStatusCode)
            return await Response<IEnumerable<SubjectTimeFromSubjectDto>>.GenerateFailedResponseAsync(response.Content);

        return await Response<IEnumerable<SubjectTimeFromSubjectDto>>.GenerateSuccessfulResponseAsync(response.Content);
    }
}