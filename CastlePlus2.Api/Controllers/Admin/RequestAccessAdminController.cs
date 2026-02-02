using CastlePlus2.Application.Auth.RequestAccess.Commands;
using CastlePlus2.Application.Auth.RequestAccess.Queries;
using CastlePlus2.Contracts.DTOs.Auth;
using CastlePlus2.Contracts.Requests.Auth;
using CastlePlus2.Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/request-access")]
    [Authorize(Policy = "AdminOnly")]
    public sealed class RequestAccessAdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RequestAccessAdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(RequestAccessDto[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList([FromQuery] string status, CancellationToken ct)
        {
            if (!Enum.TryParse<RequestAccessStatus>(status, true, out var parsedStatus))
            {
                parsedStatus = RequestAccessStatus.Pending;
            }

            var result = await _mediator.Send(new GetRequestAccessListQuery(parsedStatus), ct);
            return Ok(result);
        }

        [HttpPost("{id:int}/approve")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Approve([FromRoute] int id, [FromBody] ApproveRequestAccessRequest request, CancellationToken ct)
        {
            var approvedBy = User.FindFirst("Login")?.Value ?? User.Identity?.Name ?? "admin";
            await _mediator.Send(new ApproveRequestAccessCommand
            {
                RequestAccessId = id,
                ApprovedBy = approvedBy,
                Login = request.Login,
                Email = request.Email,
                RoleCodes = request.RoleCodes
            }, ct);

            return NoContent();
        }

        [HttpPost("{id:int}/reject")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Reject([FromRoute] int id, [FromBody] RejectRequestAccessRequest request, CancellationToken ct)
        {
            var rejectedBy = User.FindFirst("Login")?.Value ?? User.Identity?.Name ?? "admin";
            await _mediator.Send(new RejectRequestAccessCommand
            {
                RequestAccessId = id,
                RejectedBy = rejectedBy,
                Reason = request.Reason
            }, ct);

            return NoContent();
        }
    }
}