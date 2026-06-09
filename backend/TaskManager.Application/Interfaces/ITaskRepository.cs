using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces;

public interface ITaskRepository
{
    Task<List<TaskItem>> GetByUserIdAsync(Guid userId);
    Task AddAsync(TaskItem task);

}