using GameDashboardProject.Application.Features.Commands;
using GameDashboardProject.Application.Features.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameDashboardProject.API.Controllers
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

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            await _mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, "User registered successfully");
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginQueryRequest query)
        {
            var response = await _mediator.Send(query);

            if (response == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }

            if (!response.Is)
            {
                return BadRequest(new
                {
                    message = response.Message ?? "Login failed. Please check your username and password."
                });
            }

            return Ok(response);
        }


    }
}
