using System.ComponentModel.DataAnnotations;

namespace ToDoListBlazorClient.Models.DTOs;

public class RegisterDto
{
    [EmailAddress] [Required] public string Email { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
}