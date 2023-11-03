using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Services.Base;

namespace ToDoListBlazorClient.Services.Contracts;

public interface IAccountService
{
    Task<Response<UserDto>> SignInAsync(LoginDto login);
    Task<Response<UserDto>> RegisterAsync(RegisterDto register);
    Task<Response> LogoutAsync();
}