﻿using ToDoListBlazorClient.Models.DTOs;

namespace ToDoListBlazorClient.Services.Contracts;

public interface IGroupService : ISimpleHttpService<GroupDto, CreateGroupDto>
{
}