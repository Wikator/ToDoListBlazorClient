using System.Net.Http.Json;
using Blazored.LocalStorage;
using ToDoListBlazorClient.Extensions;
using ToDoListBlazorClient.Models.DTOs.SubjectTime;
using ToDoListBlazorClient.Services.Base;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Services;

public class SubjectTimeService(HttpClient httpClient, ILocalStorageService localStorage) : ISubjectTimeService
{
    private HttpClient HttpClient { get; } = httpClient;
    private ILocalStorageService LocalStorage { get; } = localStorage;
    
    public async Task<Response<SubjectTimeDto>> UpdateSubjectTimeAsync(int id,
        UpdateSubjectTimeDto updateSubjectTimeDto)
    {
        await HttpClient.AddJwtTokenAsync(LocalStorage);
        var response = await HttpClient.PutAsJsonAsync($"subject_times/{id}", updateSubjectTimeDto);

        if (!response.IsSuccessStatusCode)
            return await Response<SubjectTimeDto>.GenerateFailedResponseAsync(response.Content);

        return await Response<SubjectTimeDto>.GenerateSuccessfulResponseAsync(response.Content);
    }
}