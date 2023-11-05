using System.ComponentModel.DataAnnotations;

namespace ToDoListBlazorClient.Models.DTOs.Group;

public class CreateGroupDto
{
    [Required] public string Name { get; set; } = "New Group";
}