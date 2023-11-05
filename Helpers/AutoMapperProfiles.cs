using AutoMapper;
using ToDoListBlazorClient.Models.DTOs.Category;
using ToDoListBlazorClient.Models.DTOs.Group;
using ToDoListBlazorClient.Models.DTOs.Subject;

namespace ToDoListBlazorClient.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<CategoryDto, CreateCategoryDto>();
        CreateMap<SubjectDto, CreateSubjectDto>();
        CreateMap<GroupDto, CreateGroupDto>();
    }
}