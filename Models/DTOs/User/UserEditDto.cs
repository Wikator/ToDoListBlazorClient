using System.ComponentModel.DataAnnotations;

namespace ToDoListBlazorClient.Models.DTOs.User;

public class UserEditDto
{
    [Required]
    [StringLength(30, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
}
   