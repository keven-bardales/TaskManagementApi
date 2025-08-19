using MediatR;
using TaskManagementApi.Features.Auth.Api.Contracts;
using TaskManagementApi.Features.Auth.Application.Commands;
using TaskManagementApi.Features.Users.Domain.Entities;
using TaskManagementApi.Features.Users.Domain.Interfaces;
using TaskManagementApi.Common.Infrastructure.Authentication;
using BCrypt.Net;

namespace TaskManagementApi.Features.Auth.Application.Handlers
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public RegisterHandler(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Check if username already exists
            if (await _userRepository.UsernameExistsAsync(request.Username))
            {
                throw new InvalidOperationException("Username already exists");
            }

            // Hash the password
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Create new user
            var user = new UserEntity(request.Username, passwordHash);
            await _userRepository.AddAsync(user);

            // Generate JWT token
            var token = _jwtService.GenerateToken(user.Id.ToString(), user.Username);
            var expiresAt = DateTime.UtcNow.AddHours(1); // Match JWT settings

            return new AuthResponse
            {
                Token = token,
                Username = user.Username,
                ExpiresAt = expiresAt
            };
        }
    }
}