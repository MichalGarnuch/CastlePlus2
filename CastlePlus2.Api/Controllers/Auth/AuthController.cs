using System.Security.Claims;
using CastlePlus2.Application.Auth.ProcesyAuth.Commands.ChangePassword;
using CastlePlus2.Application.Auth.ProcesyAuth.Commands.Login;
using CastlePlus2.Application.Auth.ProcesyAuth.Commands.Refresh;
using CastlePlus2.Application.Auth.ProcesyAuth.Commands.Register;
using CastlePlus2.Application.Auth.ProcesyAuth.Queries.GetMe;
using CastlePlus2.Application.Auth.RequestAccess.Commands;
using CastlePlus2.Contracts.DTOs.Auth;
using CastlePlus2.Contracts.Requests.Auth;
using CastlePlus2.Shared.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Auth
{
    [ApiController]
    [Route("api/auth")]
    [Authorize]
    public sealed class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthTokensDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
        {
            if (request is null)
                return BadRequest("Brak danych wejściowych.");

            try
            {
                var result = await _mediator.Send(new LoginCommand
                {
                    LoginOrEmail = request.LoginOrEmail,
                    Password = request.Password,
                    DeviceInfo = request.DeviceInfo
                }, ct);

                return Ok(result.Tokens);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(BuildProblemDetails(
                    StatusCodes.Status401Unauthorized,
                    "Błąd autoryzacji",
                    ex.Message));
            }
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthTokensDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request, CancellationToken ct)
        {
            if (request is null)
                return BadRequest("Brak danych wejściowych.");

            try
            {
                var result = await _mediator.Send(new RefreshCommand
                {
                    RefreshToken = request.RefreshToken,
                    DeviceInfo = request.DeviceInfo
                }, ct);

                return Ok(result.Tokens);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(BuildProblemDetails(
                    StatusCodes.Status401Unauthorized,
                    "Błąd autoryzacji",
                    ex.Message));
            }
        }

        [HttpPost("register")]
        [Authorize(Roles = RoleCodes.Admin)]
        [ProducesResponseType(typeof(AuthTokensDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken ct)
        {
            if (request is null)
                return BadRequest("Brak danych wejściowych.");

            var result = await _mediator.Send(new RegisterCommand
            {
                Login = request.Login,
                Email = request.Email,
                Password = request.Password,
                DeviceInfo = request.DeviceInfo
            }, ct);

            return Ok(result.Tokens);
        }

        [AllowAnonymous]
        [HttpPost("request-access")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RequestAccess([FromBody] CreateRequestAccessRequest request, CancellationToken ct)
        {
            if (request is null)
                return BadRequest("Brak danych wejściowych.");

            var result = await _mediator.Send(new CreateRequestAccessCommand
            {
                FullName = request.FullName,
                Email = request.Email,
                Login = request.Login,
                Phone = request.Phone,
                Department = request.Department,
                Justification = request.Justification
            }, ct);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("activate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Activate([FromBody] ActivateAccountRequest request, CancellationToken ct)
        {
            if (request is null)
                return BadRequest("Brak danych wejściowych.");

            await _mediator.Send(new ActivateAccountCommand
            {
                Token = request.Token,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword
            }, ct);

            return NoContent();
        }

        [HttpPost("change-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request, CancellationToken ct)
        {
            if (request is null)
                return BadRequest("Brak danych wejściowych.");

            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdValue, out var userId))
            {
                return Unauthorized(BuildProblemDetails(
                    StatusCodes.Status401Unauthorized,
                    "Błąd autoryzacji",
                    "Brak identyfikatora użytkownika w tokenie."));
            }

            try
            {
                await _mediator.Send(new ChangePasswordCommand(
                    userId,
                    request.CurrentPassword,
                    request.NewPassword,
                    request.ConfirmNewPassword), ct);

                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(BuildProblemDetails(
                    StatusCodes.Status401Unauthorized,
                    "Błąd autoryzacji",
                    ex.Message));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(BuildProblemDetails(
                    StatusCodes.Status404NotFound,
                    "Nie znaleziono zasobu",
                    ex.Message));
            }
        }

        [HttpGet("me")]
        [ProducesResponseType(typeof(CurrentUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Me(CancellationToken ct)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdValue, out var userId))
            {
                return Unauthorized(BuildProblemDetails(
                    StatusCodes.Status401Unauthorized,
                    "Błąd autoryzacji",
                    "Brak identyfikatora użytkownika w tokenie."));
            }

            var result = await _mediator.Send(new GetMeQuery(userId), ct);
            return result is null
                ? Unauthorized(BuildProblemDetails(StatusCodes.Status401Unauthorized, "Błąd autoryzacji", "Brak użytkownika."))
                : Ok(result);
        }

        private ProblemDetails BuildProblemDetails(int status, string title, string detail)
        {
            var problem = new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = detail
            };

            // żeby klient widział traceId (u Ciebie TryExtractProblemMessage go obsługuje)
            problem.Extensions["traceId"] = HttpContext.TraceIdentifier;

            return problem;
        }
    }
}