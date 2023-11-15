namespace ToDoListBlazorClient.Models.DTOs.User;

// ReSharper disable once ClassNeverInstantiated.Global
public class UserDto
{
    public int Id { get; init; }
    public required string Email { get; init; }
    public string? Role { get; init; }
}