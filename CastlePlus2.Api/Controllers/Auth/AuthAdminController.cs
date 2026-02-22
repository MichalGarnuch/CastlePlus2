using System.Security.Claims;
using CastlePlus2.Application.Auth.Administracja.Commands.CreateUser;
using CastlePlus2.Application.Auth.Administracja.Commands.DeleteUser;
using CastlePlus2.Application.Auth.Administracja.Commands.RestoreUser;
using CastlePlus2.Application.Auth.Administracja.Commands.SetUserActive;
using CastlePlus2.Application.Auth.Administracja.Commands.SetUserRoles;
using CastlePlus2.Application.Auth.Administracja.Queries.GetRoles;
using CastlePlus2.Application.Auth.Administracja.Queries.GetUsersWithRoles;
using CastlePlus2.Contracts.DTOs.Auth;
using CastlePlus2.Contracts.Requests.Auth;
using CastlePlus2.Shared.Auth;
using CastlePlus2.Application.Common.Exceptions;
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


        [HttpPut("users/{id:int}/active")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SetUserActive([FromRoute] int id, [FromBody] SetUserActiveRequest request, CancellationToken ct)
        {
            if (request is null)
            {
                return BadRequest("Brak danych wejściowych.");
            }

            if (id != request.IdUzytkownika)
            {
                return BadRequest("Id użytkownika w ścieżce i body musi być takie samo.");
            }

            await _mediator.Send(new SetUserActiveCommand(id, request.CzyAktywny), ct);
            return NoContent();
        }

        [HttpPut("users/{id:int}/delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteUser([FromRoute] int id, [FromBody] DeleteUserRequest request, CancellationToken ct)
        {
            if (request is null)
            {
                return BadRequest("Brak danych wejściowych.");
            }

            if (id != request.IdUzytkownika)
            {
                return BadRequest("Id użytkownika w ścieżce i body musi być takie samo.");
            }

            var actorIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(actorIdValue, out var actorId))
            {
                return Unauthorized("Brak identyfikatora administratora w tokenie.");
            }

            var actorLogin = User.FindFirst("Login")?.Value ?? User.Identity?.Name ?? "admin";

            try
            {
                await _mediator.Send(new DeleteUserCommand(id, actorId, actorLogin), ct);
                return NoContent();
            }
            catch (BusinessConflictException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("users/{id:int}/restore")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RestoreUser([FromRoute] int id, [FromBody] RestoreUserRequest request, CancellationToken ct)
        {
            if (request is null)
            {
                return BadRequest("Brak danych wejściowych.");
            }

            if (id != request.IdUzytkownika)
            {
                return BadRequest("Id użytkownika w ścieżce i body musi być takie samo.");
            }

            await _mediator.Send(new RestoreUserCommand(id), ct);
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
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                RoleCodes = request.RoleCodes
            }, ct);

            return NoContent();
        }
    }
}
