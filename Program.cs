using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ToDoListBlazorClient;
using ToDoListBlazorClient.Extensions;
using ToDoListBlazorClient.Helpers;
using ToDoListBlazorClient.Providers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri("https://todolistrailsapi.onrender.com") });
builder.Services.AddApplicationServices();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();