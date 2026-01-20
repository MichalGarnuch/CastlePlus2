using CastlePlus2.Application.Konfiguracja.ZasobyUITeksty.Commands.CreateZasobUITekst;
using CastlePlus2.Application.Konfiguracja.ZasobyUITeksty.Commands.DeleteZasobUITekst;
using CastlePlus2.Application.Konfiguracja.ZasobyUITeksty.Commands.UpdateZasobUITekst;
using CastlePlus2.Application.Konfiguracja.ZasobyUITeksty.Queries.GetTekstyByZasob;
using CastlePlus2.Application.Konfiguracja.ZasobyUITeksty.Queries.GetZasobUITekstById;
using CastlePlus2.Contracts.DTOs.Konfiguracja;
using CastlePlus2.Contracts.Requests.Konfiguracja;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Konfiguracja
{
    [ApiController]
    [Route("api/konfiguracja/[controller]")]
    public class ZasobyUITekstyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ZasobyUITekstyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("by-encja/{idEncji:guid}")]
        public async Task<ActionResult<List<ZasobUITekstDto>>> GetByEncja([FromRoute] Guid idEncji, CancellationToken ct)
        {
            var list = await _mediator.Send(new GetTekstyByZasobQuery(idEncji), ct);
            return Ok(list);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ZasobUITekstDto>> GetById([FromRoute] long id, CancellationToken ct)
        {
            var dto = await _mediator.Send(new GetZasobUITekstByIdQuery(id), ct);
            return dto is null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ZasobUITekstDto>> Create([FromBody] CreateZasobUITekstRequest request, CancellationToken ct)
        {
            var dto = await _mediator.Send(new CreateZasobUITekstCommand
            {
                IdEncji = request.IdEncji,
                Jezyk = request.Jezyk,
                Pole = request.Pole,
                Wartosc = request.Wartosc,
                Format = request.Format
            }, ct);

            return CreatedAtAction(nameof(GetById), new { id = dto.IdZasobuTekstu }, dto);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateZasobUITekstRequest request, CancellationToken ct)
        {
            var ok = await _mediator.Send(new UpdateZasobUITekstCommand
            {
                IdZasobuTekstu = id,
                IdEncji = request.IdEncji,
                Jezyk = request.Jezyk,
                Pole = request.Pole,
                Wartosc = request.Wartosc,
                Format = request.Format
            }, ct);

            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeleteZasobUITekstCommand(id), ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
