using MediatR;
using TaskManagementApi.Features.Tasks.Application.Commands;
using TaskManagementApi.Features.Tasks.Domain.Interfaces;

namespace TaskManagementApi.Features.Tasks.Application.Handlers
{
    public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly ITaskRepository _repository;

        public DeleteTaskHandler(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            if (!await _repository.ExistsAsync(request.Id))
                throw new KeyNotFoundException($"Task with ID {request.Id} not found");

            await _repository.DeleteAsync(request.Id);
            return true;
        }
    }
}
