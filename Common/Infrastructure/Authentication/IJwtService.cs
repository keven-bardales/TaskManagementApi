using System.Security.Claims;

namespace TaskManagementApi.Common.Infrastructure.Authentication
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string username);
        ClaimsPrincipal? ValidateToken(string token);
    }
}