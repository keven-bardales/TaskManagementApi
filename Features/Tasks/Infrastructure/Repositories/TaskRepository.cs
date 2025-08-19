using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Common.Infrastructure.Persistence;
using TaskManagementApi.Features.Tasks.Domain.Interfaces;
using TaskEntity = TaskManagementApi.Features.Tasks.Domain.Entities.TaskEntity;

namespace TaskManagementApi.Features.Tasks.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaskEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<IEnumerable<TaskEntity>> GetAllAsync(bool? isCompleted = null, DateTime? dueDate = null)
        {
            var query = _context.Tasks.AsQueryable();

            if (isCompleted.HasValue)
            {
                query = query.Where(t => t.IsCompleted == isCompleted.Value);
            }

            if (dueDate.HasValue)
            {
                // Filter by date only (ignoring time)
                var dateOnly = dueDate.Value.Date;
                var nextDay = dateOnly.AddDays(1);
                query = query.Where(t => t.DueDate >= dateOnly && t.DueDate < nextDay);
            }

            return await query.OrderBy(t => t.CreatedAtUtc).ToListAsync();
        }

        public async Task<TaskEntity> AddAsync(TaskEntity task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task UpdateAsync(TaskEntity task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Tasks.AnyAsync(t => t.Id == id);
        }
    }
}