using CastlePlus2.Application.Slowniki.Waluty.Commands.CreateWaluta;
using CastlePlus2.Application.Slowniki.Waluty.Commands.UpdateWaluta;
using CastlePlus2.Application.Slowniki.Waluty.Commands.DeleteWaluta;
using CastlePlus2.Application.Slowniki.Waluty.Queries.GetAllWaluty;
using CastlePlus2.Application.Slowniki.Waluty.Queries.GetWalutaByKod;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CastlePlus2.Contracts.Requests.Slownik;

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
        public async Task<IActionResult> Create([FromBody] CreateWalutaRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new CreateWalutaCommand
            {
                KodWaluty = request.KodWaluty,
                Nazwa = request.Nazwa
            }, ct);

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

        [HttpPut("{kodWaluty}")]
        public async Task<IActionResult> Update([FromRoute] string kodWaluty, [FromBody] UpdateWalutaRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new UpdateWalutaCommand
            {
                KodWaluty = kodWaluty,
                Nazwa = request.Nazwa
            }, ct);

            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{kodWaluty}")]
        public async Task<IActionResult> Delete([FromRoute] string kodWaluty, CancellationToken ct)
        {
            await _mediator.Send(new DeleteWalutaCommand { KodWaluty = kodWaluty }, ct);
            return NoContent();
        }
    }
}
