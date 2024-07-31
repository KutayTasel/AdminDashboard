using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GameDashboardProject.Infrastructure.MongoServices;
using MediatR;
using GameDashboardProject.Application.Features.Queries.Building.GetBuildingTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using GameDashboardProject.Application.Features.Queries.GetBuildings;

namespace GameDashboardProject.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class BuildingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BuildingsController(IMediator mediator, IMongoDbService mongoDbService)
        {
            _mediator = mediator;
        }

        private bool IsUserInRole(string role)
        {
            var userClaims = HttpContext.User;
            return userClaims.IsInRole(role);
        }

        [HttpPost("addbuilding")]
        public async Task<IActionResult> AddBuilding(AddBuildingCommand request)
        {
            if (!IsUserInRole("Admin"))
            {
                return Forbid();
            }

            var response = await _mediator.Send(request);
            if (response.IsSuccess)
            {
                return StatusCode(StatusCodes.Status201Created, new { message = response.Message });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = response.Message });
            }
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllBuildingTypes()
        {
            if (!IsUserInRole("Admin"))
            {
                return Forbid();
            }

            var response = await _mediator.Send(new GetBuildingTypesQuery());
            return StatusCode(StatusCodes.Status200OK, response);
        }
        [HttpGet("getbuilding")]
        public async Task<IActionResult> GetBuildingTypes()
        {
            if (!IsUserInRole("Admin"))
            {
                return Forbid();
            }

            var response = await _mediator.Send(new GetBuildingsQuery());
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
