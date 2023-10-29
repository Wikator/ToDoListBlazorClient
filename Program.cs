using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ToDoListBlazorClient;
using ToDoListBlazorClient.Services;
using ToDoListBlazorClient.Services.Contracts;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:3000") });
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();