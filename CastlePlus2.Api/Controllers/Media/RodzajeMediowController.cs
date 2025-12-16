using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Media.RodzajeMediow.Commands.CreateRodzajMedium;
using CastlePlus2.Application.Media.RodzajeMediow.Commands.DeleteRodzajMedium;
using CastlePlus2.Application.Media.RodzajeMediow.Commands.UpdateRodzajMedium;
using CastlePlus2.Application.Media.RodzajeMediow.Queries.GetRodzajMediumById;
using CastlePlus2.Application.Media.RodzajeMediow.Queries.GetRodzajeMediow;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Media
{
    [ApiController]
    [Route("api/media/[controller]")]
    public class RodzajeMediowController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RodzajeMediowController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<RodzajMediumDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetRodzajeMediowQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{kod}")]
        [ProducesResponseType(typeof(RodzajMediumDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] string kod, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetRodzajMediumByIdQuery(kod), ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(RodzajMediumDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateRodzajMediumCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { kod = result.KodRodzaju }, result);
        }

        [HttpPut("{kod}")]
        [ProducesResponseType(typeof(RodzajMediumDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] string kod, [FromBody] UpdateRodzajMediumRequest request, CancellationToken ct)
        {
            var cmd = new UpdateRodzajMediumCommand
            {
                KodRodzaju = kod,
                Nazwa = request.Nazwa
            };

            var result = await _mediator.Send(cmd, ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("{kod}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] string kod, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeleteRodzajMediumCommand(kod), ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
