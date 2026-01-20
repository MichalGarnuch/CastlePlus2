using CastlePlus2.Application.Konfiguracja.ZasobyUI.Commands.CreateZasobUI;
using CastlePlus2.Application.Konfiguracja.ZasobyUI.Commands.DeleteZasobUI;
using CastlePlus2.Application.Konfiguracja.ZasobyUI.Commands.UpdateZasobUI;
using CastlePlus2.Application.Konfiguracja.ZasobyUI.Queries.GetAllZasobyUI;
using CastlePlus2.Application.Konfiguracja.ZasobyUI.Queries.GetPublicZasobyUI;
using CastlePlus2.Application.Konfiguracja.ZasobyUI.Queries.GetZasobUIById;
using CastlePlus2.Contracts.DTOs.Konfiguracja;
using CastlePlus2.Contracts.Requests.Konfiguracja;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Konfiguracja
{
    [ApiController]
    [Route("api/konfiguracja/[controller]")]
    public class ZasobyUIController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ZasobyUIController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ZasobUIDto>>> GetAll([FromQuery] string? typ, [FromQuery] string? kategoria, [FromQuery] bool? aktywny, CancellationToken ct)
        {
            var list = await _mediator.Send(new GetAllZasobyUIQuery
            {
                Typ = typ,
                Kategoria = kategoria,
                CzyAktywny = aktywny
            }, ct);

            return Ok(list);
        }

        [HttpGet("{idEncji:guid}")]
        public async Task<ActionResult<ZasobUIDto>> GetById([FromRoute] Guid idEncji, CancellationToken ct)
        {
            var dto = await _mediator.Send(new GetZasobUIByIdQuery(idEncji), ct);
            return dto is null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ZasobUIDto>> Create([FromBody] CreateZasobUIRequest request, CancellationToken ct)
        {
            var dto = await _mediator.Send(new CreateZasobUICommand
            {
                KodZasobu = request.KodZasobu,
                Typ = request.Typ,
                Kategoria = request.Kategoria,
                CzyAktywny = request.CzyAktywny,
                Sort = request.Sort,
                WazneOdUtc = request.WazneOdUtc,
                WazneDoUtc = request.WazneDoUtc
            }, ct);

            return CreatedAtAction(nameof(GetById), new { idEncji = dto.IdEncji }, dto);
        }

        [HttpPut("{idEncji:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid idEncji, [FromBody] UpdateZasobUIRequest request, CancellationToken ct)
        {
            var ok = await _mediator.Send(new UpdateZasobUICommand
            {
                IdEncji = idEncji,
                KodZasobu = request.KodZasobu,
                Typ = request.Typ,
                Kategoria = request.Kategoria,
                CzyAktywny = request.CzyAktywny,
                Sort = request.Sort,
                WazneOdUtc = request.WazneOdUtc,
                WazneDoUtc = request.WazneDoUtc
            }, ct);

            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{idEncji:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid idEncji, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeleteZasobUICommand(idEncji), ct);
            return ok ? NoContent() : NotFound();
        }

        [HttpGet("public")]
        public async Task<ActionResult<List<ZasobUIPublicDto>>> GetPublic([FromQuery] string typ, [FromQuery] string? kategoria, [FromQuery] string? jezyk, [FromQuery] bool includeInactive, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(typ))
            {
                return BadRequest("Parametr 'typ' jest wymagany.");
            }

            var list = await _mediator.Send(new GetPublicZasobyUIQuery
            {
                Typ = typ,
                Kategoria = kategoria,
                Jezyk = string.IsNullOrWhiteSpace(jezyk) ? "pl-PL" : jezyk,
                IncludeInactive = includeInactive
            }, ct);

            return Ok(list);
        }
    }
}