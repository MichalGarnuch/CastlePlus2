using CastlePlus2.Application.Dashboard.Najem.Queries.GetNajemDashboard;
using CastlePlus2.Contracts.DTOs.Dashboard;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Dashboard
{
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("najem")]
        [ProducesResponseType(typeof(NajemDashboardDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNajemDashboard([FromQuery] int? zakresDni, CancellationToken ct)
        {
            var days = zakresDni ?? 30;
            var result = await _mediator.Send(new GetNajemDashboardQuery(days), ct);
            return Ok(result);
        }
    }
}
