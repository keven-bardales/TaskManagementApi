using MediatR;
using TaskManagementApi.Features.Tasks.API.Contracts;

namespace TaskManagementApi.Features.Tasks.Application.Commands
{
    public class CreateTaskCommand : IRequest<TaskResponse>
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
    }

}
