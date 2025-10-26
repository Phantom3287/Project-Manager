using ProjectManagementAPI.DTOs;

namespace ProjectManagementAPI.Services
{
    public interface IProjectService
    {
        Task<List<ProjectResponseDto>> GetUserProjects(int userId);
        Task<ProjectDetailDto> GetProjectById(int projectId, int userId);
        Task<ProjectResponseDto> CreateProject(CreateProjectDto createDto, int userId);
        Task DeleteProject(int projectId, int userId);
    }
}