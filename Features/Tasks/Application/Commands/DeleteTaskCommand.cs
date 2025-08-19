using MediatR;

namespace TaskManagementApi.Features.Tasks.Application.Commands
{
    public class DeleteTaskCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
