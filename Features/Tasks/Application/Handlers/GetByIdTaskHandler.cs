using MediatR;
using TaskManagementApi.Features.Tasks.API.Contracts;
using TaskManagementApi.Features.Tasks.Application.Queries;
using TaskManagementApi.Features.Tasks.Domain.Entities;
using TaskManagementApi.Features.Tasks.Domain.Interfaces;

namespace TaskManagementApi.Features.Tasks.Application.Handlers
{
    public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, TaskResponse?>
    {
        private readonly ITaskRepository _repository;

        public GetTaskByIdHandler(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<TaskResponse?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _repository.GetByIdAsync(request.Id);
            if (task == null)
                return null;

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
