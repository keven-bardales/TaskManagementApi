using MediatR;
using TaskManagementApi.Features.Tasks.API.Contracts;

namespace TaskManagementApi.Features.Tasks.Application.Commands
{
    public class UpdateTaskCommand : IRequest<TaskResponse>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
