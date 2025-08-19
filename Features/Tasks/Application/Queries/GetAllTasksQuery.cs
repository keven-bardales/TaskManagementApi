using MediatR;
using TaskManagementApi.Features.Tasks.API.Contracts;

namespace TaskManagementApi.Features.Tasks.Application.Queries
{
    public class GetAllTasksQuery : IRequest<IEnumerable<TaskResponse>>
    {
        public bool? IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
