using ProjectManagementAPI.DTOs;

namespace ProjectManagementAPI.Services
{
    public interface ITaskService
    {
        Task<TaskResponseDto> CreateTask(int projectId, CreateTaskDto createDto, int userId);
        Task<TaskResponseDto> UpdateTask(int taskId, UpdateTaskDto updateDto, int userId);
        Task DeleteTask(int taskId, int userId);
    }
}