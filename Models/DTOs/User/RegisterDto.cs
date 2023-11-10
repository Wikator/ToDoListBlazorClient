using System.ComponentModel.DataAnnotations;

namespace ToDoListBlazorClient.Models.DTOs.User;

public class RegisterDto
{
    [EmailAddress] [Required] public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(30, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
    
    public int? GroupId { get; set; }
}