using MediatR;
using TaskManagementApi.Features.Tasks.API.Contracts;

namespace TaskManagementApi.Features.Tasks.Application.Queries
{
    public class GetTaskByIdQuery : IRequest<TaskResponse?>
    {
        public Guid Id { get; set; }
    }
}
