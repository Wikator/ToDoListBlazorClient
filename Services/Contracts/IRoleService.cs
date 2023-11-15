using ToDoListBlazorClient.Models;
using ToDoListBlazorClient.Models.DTOs.User;
using ToDoListBlazorClient.Services.Base;

namespace ToDoListBlazorClient.Services.Contracts;

public interface IRoleService
{
    Task<Response<IEnumerable<UserDto>>> GetUsers();
    Task<Response> UpdateRoleAsync(int userId, string role);
}