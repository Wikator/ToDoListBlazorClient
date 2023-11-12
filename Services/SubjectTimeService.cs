using System.Net.Http.Json;
using Blazored.LocalStorage;
using ToDoListBlazorClient.Extensions;
using ToDoListBlazorClient.Models.DTOs.SubjectTime;
using ToDoListBlazorClient.Services.Base;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services;

public class SubjectTimeService : ISubjectTimeService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public SubjectTimeService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task<Response<SubjectTimeDto>> UpdateSubjectTimeAsync(int id,
        UpdateSubjectTimeDto updateSubjectTimeDto)
    {
        await _httpClient.AddJwtTokenAsync(_localStorage);
        var response = await _httpClient.PutAsJsonAsync($"subject_times/{id}", updateSubjectTimeDto);

        if (!response.IsSuccessStatusCode)
            return await Response<SubjectTimeDto>.GenerateFailedResponseAsync(response.Content);

        return await Response<SubjectTimeDto>.GenerateSuccessfulResponseAsync(response.Content);
    }
}