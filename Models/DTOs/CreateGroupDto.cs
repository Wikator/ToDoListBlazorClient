﻿using System.ComponentModel.DataAnnotations;

namespace ToDoListBlazorClient.Models.DTOs;

public class CreateGroupDto
{
    [Required]
    public string Name { get; set; } = "New Group";
}