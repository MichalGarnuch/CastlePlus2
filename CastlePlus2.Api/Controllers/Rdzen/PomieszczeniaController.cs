using CastlePlus2.Application.Rdzen.Pomieszczenia.Commands.CreatePomieszczenie;
using CastlePlus2.Application.Rdzen.Pomieszczenia.Commands.DeletePomieszczenie;
using CastlePlus2.Application.Rdzen.Pomieszczenia.Commands.UpdatePomieszczenie;
using CastlePlus2.Application.Rdzen.Pomieszczenia.Queries.GetAllPomieszczenia;
using CastlePlus2.Application.Rdzen.Pomieszczenia.Queries.GetPomieszczenieById;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
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
        [ProducesResponseType(typeof(PomieszczenieDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PomieszczenieDto>> CreatePomieszczenie(
            [FromBody] CreatePomieszczenieRequest request, CancellationToken ct)
        {
            if (request is null) return BadRequest();

            var command = new CreatePomieszczenieCommand
            {
                IdEncjiNadrzednej = request.IdEncjiNadrzednej,
                KodPomieszczenia = request.KodPomieszczenia,
                Powierzchnia = request.Powierzchnia
            };

            var dto = await _mediator.Send(command, ct);

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
        [ProducesResponseType(typeof(PomieszczenieDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PomieszczenieDto>> GetPomieszczenieById(Guid id, CancellationToken ct)
        {
            var dto = await _mediator.Send(new GetPomieszczenieByIdQuery { Id = id }, ct);
            return dto == null ? NotFound() : Ok(dto);
        }

        // --- DODAJEMY: GET ALL ---
        [HttpGet]
        [ProducesResponseType(typeof(List<PomieszczenieDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PomieszczenieDto>>> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllPomieszczeniaQuery(), ct);
            return Ok(result);
        }

        // --- DODAJEMY: PUT ---
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePomieszczenieRequest request, CancellationToken ct)
        {
            if (request is null) return BadRequest();

            var ok = await _mediator.Send(new UpdatePomieszczenieCommand
            {
                Id = id,
                IdEncjiNadrzednej = request.IdEncjiNadrzednej,
                KodPomieszczenia = request.KodPomieszczenia,
                Powierzchnia = request.Powierzchnia
            }, ct);

            return ok ? NoContent() : NotFound();
        }

        // --- DODAJEMY: DELETE ---
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeletePomieszczenieCommand { Id = id }, ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
