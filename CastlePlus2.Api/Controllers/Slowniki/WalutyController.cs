using CastlePlus2.Application.Slowniki.Waluty.Commands.CreateWaluta;
using CastlePlus2.Application.Slowniki.Waluty.Queries.GetAllWaluty;
using CastlePlus2.Application.Slowniki.Waluty.Queries.GetWalutaByKod;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Slowniki
{
    [ApiController]
    [Route("api/slowniki/[controller]")]
    public class WalutyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WalutyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWalutaCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetByKod), new { kodWaluty = result.KodWaluty }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllWalutyQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{kodWaluty}")]
        public async Task<IActionResult> GetByKod([FromRoute] string kodWaluty, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetWalutaByKodQuery { KodWaluty = kodWaluty }, ct);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
