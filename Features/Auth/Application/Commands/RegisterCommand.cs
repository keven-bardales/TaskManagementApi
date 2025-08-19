using MediatR;
using TaskManagementApi.Features.Auth.Api.Contracts;

namespace TaskManagementApi.Features.Auth.Application.Commands
{
    public class RegisterCommand : IRequest<AuthResponse>
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}