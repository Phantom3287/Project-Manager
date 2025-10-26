using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Data;
using ProjectManagementAPI.DTOs;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaskResponseDto> CreateTask(int projectId, CreateTaskDto createDto, int userId)
        {
            // Verify project belongs to user
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == projectId && p.UserId == userId);

            if (project == null)
            {
                throw new Exception("Project not found");
            }

            var task = new ProjectTask
            {
                Title = createDto.Title,
                DueDate = createDto.DueDate,
                ProjectId = projectId,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                DueDate = task.DueDate,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt,
                ProjectId = task.ProjectId
            };
        }

        public async Task<TaskResponseDto> UpdateTask(int taskId, UpdateTaskDto updateDto, int userId)
        {
            var task = await _context.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null || task.Project.UserId != userId)
            {
                throw new Exception("Task not found");
            }

            task.Title = updateDto.Title;
            task.DueDate = updateDto.DueDate;
            task.IsCompleted = updateDto.IsCompleted;

            await _context.SaveChangesAsync();

            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                DueDate = task.DueDate,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt,
                ProjectId = task.ProjectId
            };
        }

        public async Task DeleteTask(int taskId, int userId)
        {
            var task = await _context.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null || task.Project.UserId != userId)
            {
                throw new Exception("Task not found");
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}