﻿using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace ToDoListBlazorClient.Extensions;

public static class HttpClientExtensions
{
    public static async Task AddJwtTokenAsync(this HttpClient httpClient,
        ILocalStorageService localStorage)
    {
        var token = await localStorage.GetItemAsync<string>("accessToken");

        httpClient.DefaultRequestHeaders.Authorization = token switch
        {
            null => null,
            _ => new AuthenticationHeaderValue("Bearer", token)
        };
    }
}