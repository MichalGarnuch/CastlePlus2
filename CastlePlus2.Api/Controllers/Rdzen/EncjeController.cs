using CastlePlus2.Application.Rdzen.Encje.Commands.CreateEncja;
using CastlePlus2.Application.Rdzen.Encje.Commands.DeleteEncja;
using CastlePlus2.Application.Rdzen.Encje.Commands.UpdateEncja;
using CastlePlus2.Application.Rdzen.Encje.Queries.GetAllEncje;
using CastlePlus2.Application.Rdzen.Encje.Queries.GetEncjaById;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Rzden
{
    [ApiController]
    [Route("api/[controller]")]
    public class EncjeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EncjeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Encje
        [HttpGet]
        [ProducesResponseType(typeof(List<EncjaDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<EncjaDto>>> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllEncjeQuery(), ct);
            return Ok(result);
        }

        // GET: api/Encje/{id}
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(EncjaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetEncjaByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        // POST: api/Encje
        [HttpPost]
        [ProducesResponseType(typeof(EncjaDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateEncjaRequest request, CancellationToken ct)
        {
            if (request is null) return BadRequest();

            var command = new CreateEncjaCommand
            {
                TypEncji = request.TypEncji,
                KodEncji = request.KodEncji
            };

            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        // PUT: api/Encje/{id}
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateEncjaRequest request, CancellationToken ct)
        {
            if (request is null) return BadRequest();

            var ok = await _mediator.Send(new UpdateEncjaCommand
            {
                Id = id,
                TypEncji = request.TypEncji,
                KodEncji = request.KodEncji
            }, ct);

            return ok ? NoContent() : NotFound();
        }

        // DELETE: api/Encje/{id}
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeleteEncjaCommand(id), ct);
            return ok ? NoContent() : NotFound();
        }
    }
}