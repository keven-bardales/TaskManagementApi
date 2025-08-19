using MediatR;
using TaskManagementApi.Features.Tasks.API.Contracts;
using TaskManagementApi.Features.Tasks.Application.Commands;
using TaskManagementApi.Features.Tasks.Domain.Entities;
using TaskManagementApi.Features.Tasks.Domain.Interfaces;

namespace TaskManagementApi.Features.Tasks.Application.Handlers
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, TaskResponse>
    {
        private readonly ITaskRepository _repository;

        public UpdateTaskHandler(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<TaskResponse> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _repository.GetByIdAsync(request.Id);
            if (task == null)
                throw new KeyNotFoundException($"Task with ID {request.Id} not found");

            task.Update(request.Title, request.Description, request.IsCompleted, request.DueDate);
            await _repository.UpdateAsync(task);

            return MapToResponse(task);
        }

        private TaskResponse MapToResponse(TaskEntity task)
        {
            return new TaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAtUtc,
                UpdatedAt = task.UpdatedAtUtc
            };
        }
    }
}
