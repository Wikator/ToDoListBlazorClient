using System.Net.Http.Json;
using ToDoListBlazorClient.Extensions;
using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services;

public class SubjectService : ISubjectService
{
    private readonly HttpClient _http;

    public SubjectService(HttpClient http, IAccountService accountService)
    {
        _http = http;

        // accountService.SetUserAsync();
    }

    public async Task<IEnumerable<SubjectDto>> GetSubjects()
    {
        var response = await _http.GetAsync("subjects");

        return await response.ReadJsonFromBodyAsync<IEnumerable<SubjectDto>>();
    }

    public async Task<SubjectDto> GetSubject(int id)
    {
        var response = await _http.GetAsync($"subjects/{id}");

        return await response.ReadJsonFromBodyAsync<SubjectDto>();
    }

    public async Task<SubjectDto> CreateSubject(CreateSubjectDto subject)
    {
        var response = await _http.PostAsJsonAsync("subjects", subject);

        return await response.ReadJsonFromBodyAsync<SubjectDto>();
    }

    public async Task<SubjectDto> UpdateSubject(CreateSubjectDto subject)
    {
        var response = await _http.PutAsJsonAsync("subjects", subject);

        return await response.ReadJsonFromBodyAsync<SubjectDto>();
    }

    public async Task DeleteGroup(int id)
    {
        var response = await _http.DeleteAsync($"subjects/{id}");

        if (!response.IsSuccessStatusCode)
            throw new Exception(await response.Content.ReadAsStringAsync());
    }
}