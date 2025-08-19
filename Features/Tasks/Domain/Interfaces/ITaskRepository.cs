using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskEntity = TaskManagementApi.Features.Tasks.Domain.Entities.TaskEntity;

namespace TaskManagementApi.Features.Tasks.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<TaskEntity>> GetAllAsync(bool? isCompleted = null, DateTime? dueDate = null);
        Task<TaskEntity> AddAsync(TaskEntity task);
        Task UpdateAsync(TaskEntity task);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}