using ToDoListBlazorClient.Models.DTOs;

namespace ToDoListBlazorClient.Services.Contracts;

public interface IAccountService
{
    Task<UserDto?> SignInAsync(LoginDto login);
    Task<UserDto?> RegisterAsync(RegisterDto register);
    Task LogoutAsync();
    Task SetUserAsync();
}