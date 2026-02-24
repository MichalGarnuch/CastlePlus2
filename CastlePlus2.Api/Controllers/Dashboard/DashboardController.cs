using CastlePlus2.Application.Dashboard.Najem.Queries.GetDashboardV1Najem;
using CastlePlus2.Application.Dashboard.Najem.Queries.GetNajemDashboard;
using CastlePlus2.Application.Dashboard.Najem.Queries.GetNajemPowerDashboard;
using CastlePlus2.Contracts.DTOs.Dashboard;
using CastlePlus2.Contracts.Requests.Dashboard;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CastlePlus2.Api.Controllers.Dashboard
{
    [ApiController]
    [Authorize]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("najem")]
        [Authorize(Policy = "EmployerOrAdmin")]
        [ProducesResponseType(typeof(NajemDashboardDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNajemDashboard([FromQuery] int? zakresDni, CancellationToken ct)
        {
            var days = zakresDni ?? 30;
            var result = await _mediator.Send(new GetNajemDashboardQuery(days), ct);
            return Ok(result);
        }


        [HttpPost("najem-power")]
        [Authorize(Policy = "EmployerOrAdmin")]
        [ProducesResponseType(typeof(NajemPowerDashboardDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNajemPowerDashboard([FromBody] GetNajemPowerDashboardRequest? request, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetNajemPowerDashboardQuery(request ?? new GetNajemPowerDashboardRequest()), ct);
            return Ok(result);
        }

        [HttpGet("v1/najem")]
        [Authorize(Policy = "EmployerOrAdmin")]
        [ProducesResponseType(typeof(DashboardV1NajemDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDashboardV1Najem(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetDashboardV1NajemQuery(), ct);
            return Ok(result);
        }
    }
}
