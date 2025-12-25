using CastlePlus2.Application.Utrzymanie.ZleceniaPracy.Commands.CreateZleceniePracy;
using CastlePlus2.Application.Utrzymanie.ZleceniaPracy.Commands.DeleteZleceniePracy;
using CastlePlus2.Application.Utrzymanie.ZleceniaPracy.Commands.UpdateZleceniePracy;
using CastlePlus2.Application.Utrzymanie.ZleceniaPracy.Queries.GetAllZleceniaPracy;
using CastlePlus2.Application.Utrzymanie.ZleceniaPracy.Queries.GetZleceniePracyById;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using CastlePlus2.Contracts.Requests.Utrzymanie;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Utrzymanie
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZleceniaPracyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ZleceniaPracyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ZleceniePracyDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var list = await _mediator.Send(new GetAllZleceniaPracyQuery(), ct);
            return Ok(list);
        }

        /// <summary>
        /// Zwraca zlecenie pracy po IdZlecenia (bigint).
        /// </summary>
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(ZleceniePracyDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ZleceniePracyDto>> GetById(long id, CancellationToken ct)
        {
            var dto = await _mediator.Send(new GetZleceniePracyByIdQuery(id), ct);

            if (dto is null)
                return NotFound();

            return Ok(dto);
        }

        /// <summary>
        /// Tworzy nowe zlecenie pracy.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ZleceniePracyDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ZleceniePracyDto>> Create([FromBody] CreateZleceniePracyRequest request, CancellationToken ct)
        {
            var dto = await _mediator.Send(new CreateZleceniePracyCommand
            {
                IdEncjiGospodarza = request.IdEncjiGospodarza,
                Tytul = request.Tytul,
                Opis = request.Opis,
                Status = request.Status,
                DataZamkniecia = request.DataZamkniecia
            }, ct);

            return CreatedAtAction(
                nameof(GetById),
                new { id = dto.IdZlecenia },
                dto);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(ZleceniePracyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateZleceniePracyRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new UpdateZleceniePracyCommand
            {
                IdZlecenia = id,
                IdEncjiGospodarza = request.IdEncjiGospodarza,
                Tytul = request.Tytul,
                Opis = request.Opis,
                Status = request.Status,
                DataZamkniecia = request.DataZamkniecia
            }, ct);

            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken ct)
        {
            await _mediator.Send(new DeleteZleceniePracyCommand { IdZlecenia = id }, ct);
            return NoContent();
        }
    }
}