using ToDoListBlazorClient.Models.DTOs;

namespace ToDoListBlazorClient.Services.Contracts;

public interface IGroupService
{
    Task<IEnumerable<GroupDto>> GetGroups();

    Task<GroupDto> GetGroup(int id);

    Task<GroupDto> CreateGroup(CreateGroupDto group);

    Task<GroupDto> UpdateGroup(int id, CreateGroupDto group);

    Task DeleteGroup(int id);
}