using System.Security.Claims;
using CastlePlus2.Application.Auth.ProcesyAuth.Commands.Login;
using CastlePlus2.Application.Auth.ProcesyAuth.Commands.Refresh;
using CastlePlus2.Application.Auth.ProcesyAuth.Commands.Register;
using CastlePlus2.Application.Auth.ProcesyAuth.Queries.GetMe;
using CastlePlus2.Application.Auth.ProcesyAuth.Commands.Register;
using CastlePlus2.Contracts.DTOs.Auth;
using CastlePlus2.Contracts.Requests.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Auth
{
    [ApiController]
    [Route("api/auth")]
    public sealed class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthTokensDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
        {
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
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpPost("refresh")]
        [ProducesResponseType(typeof(AuthTokensDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request, CancellationToken ct)
        {
            try
            {
                var result = await _mediator.Send(new RefreshCommand
                {
                    RefreshToken = request.RefreshToken,
                    DeviceInfo = request.DeviceInfo
                }, ct);

                return Ok(result.Tokens);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthTokensDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new RegisterCommand
            {
                Login = request.Login,
                Email = request.Email,
                Password = request.Password,
                DeviceInfo = request.DeviceInfo
            }, ct);

            return Ok(result.Tokens);
        }

        [Authorize]
        [HttpGet("me")]
        [ProducesResponseType(typeof(CurrentUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Me(CancellationToken ct)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdValue, out var userId))
                return Unauthorized();

            var result = await _mediator.Send(new GetMeQuery(userId), ct);
            return result is null ? Unauthorized() : Ok(result);
        }
    }
}
