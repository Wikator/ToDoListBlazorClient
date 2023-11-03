using ToDoListBlazorClient.Models.DTOs;
using ToDoListBlazorClient.Services.Base;

namespace ToDoListBlazorClient.Services.Contracts;

public interface IGroupService
{
    Task<Response<IEnumerable<GroupDto>>> GetGroups();

    Task<Response<GroupDto>> GetGroup(int id);

    Task<Response<GroupDto>> CreateGroup(CreateGroupDto group);

    Task<Response<GroupDto>> UpdateGroup(int id, CreateGroupDto group);

    Task<Response> DeleteGroup(int id);
}