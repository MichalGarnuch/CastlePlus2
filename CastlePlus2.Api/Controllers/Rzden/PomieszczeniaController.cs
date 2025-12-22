using CastlePlus2.Application.Rdzen.Pomieszczenia.Commands.CreatePomieszczenie;
using CastlePlus2.Application.Rdzen.Pomieszczenia.Commands.DeletePomieszczenie;
using CastlePlus2.Application.Rdzen.Pomieszczenia.Commands.UpdatePomieszczenie;
using CastlePlus2.Application.Rdzen.Pomieszczenia.Queries.GetAllPomieszczenia;
using CastlePlus2.Application.Rdzen.Pomieszczenia.Queries.GetPomieszczenieById;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CastlePlus2.Api.Controllers.Rzden
{
    [ApiController]
    [Route("api/[controller]")]
    public class PomieszczeniaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PomieszczeniaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Tworzy nowe pomieszczenie w lokalu.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(PomieszczenieDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PomieszczenieDto>> CreatePomieszczenie(
            [FromBody] CreatePomieszczenieCommand command)
        {
            var dto = await _mediator.Send(command);

            // Zwracamy 201 Created z lokalizacją zasobu
            return CreatedAtAction(
                nameof(GetPomieszczenieById),
                new { id = dto.Id },
                dto);
        }

        /// <summary>
        /// Zwraca pojedyncze pomieszczenie po Id.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PomieszczenieDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PomieszczenieDto>> GetPomieszczenieById(Guid id)
        {
            var dto = await _mediator.Send(new GetPomieszczenieByIdQuery { Id = id });

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        // --- DODAJEMY: GET ALL ---
        [HttpGet]
        [ProducesResponseType(typeof(List<PomieszczenieDto>), 200)]
        public async Task<ActionResult<List<PomieszczenieDto>>> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllPomieszczeniaQuery(), ct);
            return Ok(result);
        }

        // --- DODAJEMY: PUT ---
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(PomieszczenieDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PomieszczenieDto>> Update(Guid id, [FromBody] UpdatePomieszczenieCommand command, CancellationToken ct)
        {
            command.Id = id;

            var dto = await _mediator.Send(command, ct);
            if (dto is null)
                return NotFound();

            return Ok(dto);
        }

        // --- DODAJEMY: DELETE ---
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeletePomieszczenieCommand { Id = id }, ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
