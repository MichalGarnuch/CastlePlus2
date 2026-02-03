using CastlePlus2.Application.Auth.Administracja.Commands.CreateUser;
using CastlePlus2.Application.Auth.Administracja.Commands.SetUserRoles;
using CastlePlus2.Application.Auth.Administracja.Queries.GetRoles;
using CastlePlus2.Application.Auth.Administracja.Queries.GetUsersWithRoles;
using CastlePlus2.Contracts.DTOs.Auth;
using CastlePlus2.Contracts.Requests.Auth;
using CastlePlus2.Shared.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Auth
{
    [ApiController]
    [Route("api/auth/admin")]
    [Authorize(Roles = RoleCodes.Admin)]
    public sealed class AuthAdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthAdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("users")]
        [ProducesResponseType(typeof(AdminUserDto[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers(CancellationToken ct)
        {
            var users = await _mediator.Send(new GetUsersWithRolesQuery(), ct);
            return Ok(users);
        }

        [HttpGet("roles")]
        [ProducesResponseType(typeof(RoleDto[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoles(CancellationToken ct)
        {
            var roles = await _mediator.Send(new GetRolesQuery(), ct);
            return Ok(roles);
        }

        [HttpPut("users/{id:int}/roles")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetUserRoles([FromRoute] int id, [FromBody] SetUserRolesRequest request, CancellationToken ct)
        {
            if (request is null)
            {
                return BadRequest("Brak danych wejściowych.");
            }

            await _mediator.Send(new SetUserRolesCommand(id, request.RoleCodes), ct);
            return NoContent();
        }

        [HttpPost("users")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken ct)
        {
            if (request is null)
            {
                return BadRequest("Brak danych wejściowych.");
            }

            var createdBy = User.FindFirst("Login")?.Value ?? User.Identity?.Name ?? "admin";
            await _mediator.Send(new CreateUserCommand
            {
                CreatedBy = createdBy,
                Login = request.Login,
                Email = request.Email,
                RoleCodes = request.RoleCodes
            }, ct);

            return NoContent();
        }
    }
}
