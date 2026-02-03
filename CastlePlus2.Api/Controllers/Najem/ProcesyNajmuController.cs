using CastlePlus2.Application.Najem.ProcesyNajmu.Commands.AneksujCzynsz;
using CastlePlus2.Application.Najem.ProcesyNajmu.Commands.ZakonczUmoweNajmu;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CastlePlus2.Api.Controllers.Najem
{
    [ApiController]
    [Authorize]
    [Route("api/najem/procesy")]
    public class ProcesyNajmuController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProcesyNajmuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("umowy/{idUmowy:guid}/czynsz/aneks")]
        [ProducesResponseType(typeof(AneksujCzynszResult), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AneksujCzynsz([FromRoute] Guid idUmowy, [FromBody] AneksujCzynszRequest request, CancellationToken ct)
        {
            if (request is null) return BadRequest();

            var result = await _mediator.Send(new AneksujCzynszCommand
            {
                IdUmowyNajmu = idUmowy,
                Nazwa = request.Nazwa,
                KodJednostki = request.KodJednostki,
                Stawka = request.Stawka,
                IloscBazowa = request.IloscBazowa,
                KodIndeksacji = request.KodIndeksacji,
                OdDnia = request.OdDnia
            }, ct);

            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPost("umowy/{idUmowy:guid}/zakoncz")]
        [ProducesResponseType(typeof(ZakonczUmoweNajmuResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ZakonczUmowe([FromRoute] Guid idUmowy, [FromBody] ZakonczUmoweNajmuRequest request, CancellationToken ct)
        {
            if (request is null) return BadRequest();

            var result = await _mediator.Send(new ZakonczUmoweNajmuCommand
            {
                IdUmowyNajmu = idUmowy,
                DataZakonczenia = request.DataZakonczenia,
                KwotaZwrotuKaucji = request.KwotaZwrotuKaucji
            }, ct);

            return Ok(result);
        }
    }
}