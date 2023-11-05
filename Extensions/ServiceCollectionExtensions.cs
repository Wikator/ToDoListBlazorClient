using ToDoListBlazorClient.Services;
using ToDoListBlazorClient.Services.Contracts;

namespace ToDoListBlazorClient.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<ISubjectService, SubjectService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ITaskService, TaskService>();

        return services;
    }
}