using MediatR;
using TaskManagementApi.Features.Auth.Api.Contracts;
using TaskManagementApi.Features.Auth.Application.Commands;
using TaskManagementApi.Features.Users.Domain.Interfaces;
using TaskManagementApi.Common.Infrastructure.Authentication;
using BCrypt.Net;

namespace TaskManagementApi.Features.Auth.Application.Handlers
{
    public class LoginHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public LoginHandler(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // Find user by username
            var user = await _userRepository.GetByUsernameAsync(request.Username);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            // Verify password
            var isPasswordValid = user.VerifyPassword(request.Password, 
                (password, hash) => BCrypt.Net.BCrypt.Verify(password, hash));

            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

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