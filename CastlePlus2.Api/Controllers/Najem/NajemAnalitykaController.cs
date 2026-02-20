using CastlePlus2.Application.Najem.Analityka.Queries.GetOblozenieLokaliUtcDzis;
using CastlePlus2.Application.Najem.Analityka.Queries.GetRaportNajmuZaMiesiac;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;
using CastlePlus2.Shared.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Najem
{
    [ApiController]
    [Route("api/najem/analityka")]
    [Authorize(Roles = RoleCodes.AdminOrEmployee)]
    public class NajemAnalitykaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NajemAnalitykaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("oblozenie-utc-dzis")]
        [ProducesResponseType(typeof(IReadOnlyList<OblozenieLokaluDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOblozenieLokaliUtcDzis(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetOblozenieLokaliUtcDzisQuery(), ct);
            return Ok(result);
        }

        [HttpPost("raport-miesiac")]
        [ProducesResponseType(typeof(IReadOnlyList<RaportNajmuZaMiesiacRowDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRaportNajmuZaMiesiac([FromBody] GetRaportNajmuZaMiesiacRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetRaportNajmuZaMiesiacQuery(request), ct);
            return Ok(result);
        }
    }
}