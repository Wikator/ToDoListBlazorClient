using ToDoListBlazorClient.Models.DTOs.Group;

namespace ToDoListBlazorClient.Services.Contracts;

public interface IGroupService : ISimpleHttpService<GroupDto, CreateGroupDto>;