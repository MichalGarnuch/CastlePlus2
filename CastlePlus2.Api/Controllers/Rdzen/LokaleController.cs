using CastlePlus2.Application.Rdzen.Lokale.Commands.CreateLokal;
using CastlePlus2.Application.Rdzen.Lokale.Commands.DeleteLokal;
using CastlePlus2.Application.Rdzen.Lokale.Commands.UpdateLokal;
using CastlePlus2.Application.Rdzen.Lokale.Queries.GetAllLokale;
using CastlePlus2.Application.Rdzen.Lokale.Queries.GetLokalById;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Rzden
{
    [ApiController]
    [Route("api/[controller]")]
    public class LokaleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LokaleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Lokale
        [HttpGet]
        [ProducesResponseType(typeof(List<LokalDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<LokalDto>>> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllLokaleQuery(), ct);
            return Ok(result);
        }

        // GET: api/Lokale/{id}
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(LokalDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id, CancellationToken ct)
        {
            // ważne: query ma konstruktor i read-only Id
            var result = await _mediator.Send(new GetLokalByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        // POST: api/Lokale
        [HttpPost]
        [ProducesResponseType(typeof(LokalDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateLokalRequest request, CancellationToken ct)
        {
            if (request is null) return BadRequest();

            var command = new CreateLokalCommand
            {
                IdBudynku = request.IdBudynku,
                KodLokalu = request.KodLokalu,
                Powierzchnia = request.Powierzchnia,
                Przeznaczenie = request.Przeznaczenie
            };

            var result = await _mediator.Send(command, ct);
            if (result is null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        // PUT: api/Lokale/{id}
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateLokalRequest request, CancellationToken ct)
        {
            if (request is null) return BadRequest();

            var ok = await _mediator.Send(new UpdateLokalCommand
            {
                Id = id,
                IdBudynku = request.IdBudynku,
                KodLokalu = request.KodLokalu,
                Powierzchnia = request.Powierzchnia,
                Przeznaczenie = request.Przeznaczenie
            }, ct);

            return ok ? NoContent() : NotFound();
        }

        // DELETE: api/Lokale/{id}
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeleteLokalCommand(id), ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
