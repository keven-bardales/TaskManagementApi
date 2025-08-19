using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Features.Auth.Api.Contracts;
using TaskManagementApi.Features.Auth.Application.Commands;

namespace TaskManagementApi.Features.Auth.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="request">Registration data</param>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new RegisterCommand
            {
                Username = request.Username,
                Password = request.Password
            };

            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating user", error = ex.Message });
            }
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="request">Login credentials</param>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new LoginCommand
            {
                Username = request.Username,
                Password = request.Password
            };

            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error during login", error = ex.Message });
            }
        }
    }
}