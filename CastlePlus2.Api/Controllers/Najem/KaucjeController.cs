using CastlePlus2.Application.Najem.Kaucje.Commands.CreateKaucja;
using CastlePlus2.Application.Najem.Kaucje.Commands.DeleteKaucja;
using CastlePlus2.Application.Najem.Kaucje.Commands.UpdateKaucja;
using CastlePlus2.Application.Najem.Kaucje.Queries.GetAllKaucje;
using CastlePlus2.Application.Najem.Kaucje.Queries.GetKaucjaById;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Najem
{
    [ApiController]
    [Route("api/najem/[controller]")]
    public class KaucjeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public KaucjeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(KaucjaDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateKaucjaRequest request, CancellationToken ct)
        {
            var command = new CreateKaucjaCommand
            {
                IdUmowyNajmu = request.IdUmowyNajmu,
                RodzajOperacji = request.RodzajOperacji,
                Kwota = request.Kwota,
                KodWaluty = request.KodWaluty,
                DataOperacji = request.DataOperacji
            };

            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdOperacjiKaucji }, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<KaucjaDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllKaucjeQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(KaucjaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetKaucjaByIdQuery(id), ct);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(KaucjaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateKaucjaRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new UpdateKaucjaCommand(id, request), ct);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeleteKaucjaCommand(id), ct);
            return ok ? Ok() : NotFound();
        }
    }
}