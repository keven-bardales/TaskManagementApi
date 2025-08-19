using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Features.Auth.Api.Contracts
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Username is required")]
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters long")]
        [MaxLength(50, ErrorMessage = "Username cannot be longer than 50 characters")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; } = string.Empty;
    }
}