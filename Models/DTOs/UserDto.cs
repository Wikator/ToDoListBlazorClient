namespace ToDoListBlazorClient.Models.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public string Token { get; set; }
}