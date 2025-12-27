using CastlePlus2.Application.Najem.PrzedmiotyNajmu.Commands.CreatePrzedmiotNajmu;
using CastlePlus2.Application.Najem.PrzedmiotyNajmu.Commands.DeletePrzedmiotNajmu;
using CastlePlus2.Application.Najem.PrzedmiotyNajmu.Commands.UpdatePrzedmiotNajmu;
using CastlePlus2.Application.Najem.PrzedmiotyNajmu.Queries.GetAllPrzedmiotyNajmu;
using CastlePlus2.Application.Najem.PrzedmiotyNajmu.Queries.GetPrzedmiotNajmuById;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Najem
{
    [ApiController]
    [Route("api/najem/[controller]")]
    public class PrzedmiotyNajmuController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PrzedmiotyNajmuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PrzedmiotNajmuDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllPrzedmiotyNajmuQuery(), ct);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PrzedmiotNajmuDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreatePrzedmiotNajmuRequest request, CancellationToken ct)
        {
            if (request is null) return BadRequest();

            var result = await _mediator.Send(new CreatePrzedmiotNajmuCommand
            {
                IdUmowyNajmu = request.IdUmowyNajmu,
                IdEncji = request.IdEncji,
                UdzialProcent = request.UdzialProcent,
                OdDnia = request.OdDnia,
                DoDnia = request.DoDnia
            }, ct);

            return CreatedAtAction(nameof(GetById), new { id = result.IdPrzedmiotuNajmu }, result);
        }

        // TA WERSJA JEST POPRAWNA (Zostawiamy)
        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdatePrzedmiotNajmuRequest request, CancellationToken ct)
        {
            if (request is null) return BadRequest();

            var ok = await _mediator.Send(new UpdatePrzedmiotNajmuCommand
            {
                IdPrzedmiotuNajmu = id,
                IdUmowyNajmu = request.IdUmowyNajmu,
                IdEncji = request.IdEncji,
                UdzialProcent = request.UdzialProcent,
                OdDnia = request.OdDnia,
                DoDnia = request.DoDnia
            }, ct);

            return ok ? NoContent() : NotFound();
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetPrzedmiotNajmuByIdQuery(id), ct);
            return result == null ? NotFound() : Ok(result);
        }

        // [USUNIĘTO BŁĘDNĄ METODĘ UPDATE TUTAJ]

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeletePrzedmiotNajmuCommand(id), ct);
            return ok ? NoContent() : NotFound();
        }
    }
}