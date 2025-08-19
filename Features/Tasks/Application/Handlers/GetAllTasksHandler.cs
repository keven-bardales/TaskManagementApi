using MediatR;
using TaskManagementApi.Features.Tasks.API.Contracts;
using TaskManagementApi.Features.Tasks.Application.Queries;
using TaskManagementApi.Features.Tasks.Domain.Interfaces;

namespace TaskManagementApi.Features.Tasks.Application.Handlers
{
    public class GetAllTasksHandler : IRequestHandler<GetAllTasksQuery, IEnumerable<TaskResponse>>
    {
        private readonly ITaskRepository _repository;

        public GetAllTasksHandler(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaskResponse>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _repository.GetAllAsync(request.IsCompleted, request.DueDate);

            return tasks.Select(task => new TaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAtUtc,
                UpdatedAt = task.UpdatedAtUtc
            });
        }
    }
}
