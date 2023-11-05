using System.ComponentModel.DataAnnotations;

namespace ToDoListBlazorClient.Models.DTOs.User;

public class LoginDto
{
    [EmailAddress] [Required] public string Email { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
}