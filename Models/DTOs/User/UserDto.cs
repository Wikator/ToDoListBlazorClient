namespace ToDoListBlazorClient.Models.DTOs.User;

// ReSharper disable once ClassNeverInstantiated.Global
public class UserDto
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public string? Role { get; set; }
}