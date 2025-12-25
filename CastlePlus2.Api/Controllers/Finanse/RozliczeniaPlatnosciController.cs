using CastlePlus2.Application.Finanse.RozliczeniaPlatnosci.Commands.CreateRozliczeniePlatnosci;
using CastlePlus2.Application.Finanse.RozliczeniaPlatnosci.Commands.DeleteRozliczeniePlatnosci;
using CastlePlus2.Application.Finanse.RozliczeniaPlatnosci.Commands.UpdateRozliczeniePlatnosci;
using CastlePlus2.Application.Finanse.RozliczeniaPlatnosci.Queries.GetAllRozliczeniaPlatnosci;
using CastlePlus2.Application.Finanse.RozliczeniaPlatnosci.Queries.GetRozliczeniePlatnosciById;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Finanse
{
    [ApiController]
    [Route("api/finanse/[controller]")]
    public class RozliczeniaPlatnosciController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RozliczeniaPlatnosciController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<RozliczeniePlatnosciDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllRozliczeniaPlatnosciQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(RozliczeniePlatnosciDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetRozliczeniePlatnosciByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(RozliczeniePlatnosciDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateRozliczeniePlatnosciRequest request, CancellationToken ct)
        {
            var cmd = new CreateRozliczeniePlatnosciCommand
            {
                IdPlatnosci = request.IdPlatnosci,
                IdFaktury = request.IdFaktury,
                Kwota = request.Kwota
            };

            var result = await _mediator.Send(cmd, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdRozliczenia }, result);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(RozliczeniePlatnosciDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateRozliczeniePlatnosciRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new UpdateRozliczeniePlatnosciCommand
            {
                IdRozliczenia = id,
                IdPlatnosci = request.IdPlatnosci,
                IdFaktury = request.IdFaktury,
                Kwota = request.Kwota
            }, ct);

            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken ct)
        {
            await _mediator.Send(new DeleteRozliczeniePlatnosciCommand { IdRozliczenia = id }, ct);
            return NoContent();
        }
    }
}
