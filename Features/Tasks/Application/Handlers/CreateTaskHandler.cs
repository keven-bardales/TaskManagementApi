using MediatR;
using TaskManagementApi.Features.Tasks.API.Contracts;
using TaskManagementApi.Features.Tasks.Application.Commands;
using TaskManagementApi.Features.Tasks.Domain.Entities;
using TaskManagementApi.Features.Tasks.Domain.Interfaces;

namespace TaskManagementApi.Features.Tasks.Application.Handlers
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, TaskResponse>
    {
        private readonly ITaskRepository _repository;

        public CreateTaskHandler(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<TaskResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new TaskEntity(request.Title, request.Description, request.DueDate);
            var createdTask = await _repository.AddAsync(task);

            return MapToResponse(createdTask);
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
