using System.Collections.Generic;
using System.Linq;
using CastlePlus2.Application.Finanse.ProcesyFaktury.Commands.WystawFakture;
using CastlePlus2.Application.Finanse.ProcesyFaktury.Queries.GetWystawFaktureContext;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Finanse
{
    [ApiController]
    [Route("api/finanse/procesy/faktury")]
    public class ProcesyFakturyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProcesyFakturyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("context")]
        [ProducesResponseType(typeof(WystawFaktureContextDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContext(CancellationToken ct)
        {
            var context = await _mediator.Send(new GetWystawFaktureContextQuery(), ct);
            return Ok(context);
        }

        [HttpPost("wystaw")]
        [ProducesResponseType(typeof(WystawFaktureResultDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Wystaw([FromBody] WystawFaktureRequest request, CancellationToken ct)
        {
            var pozycje = request.Pozycje ?? new List<WystawFakturePozycjaRequest>();

            var result = await _mediator.Send(new WystawFaktureCommand
            {
                NumerFaktury = request.NumerFaktury,
                IdPodmiotu = request.IdPodmiotu,
                DataWystawienia = request.DataWystawienia,
                DataSprzedazy = request.DataSprzedazy,
                KodWaluty = request.KodWaluty,
                Pozycje = pozycje.Select(pozycja => new WystawFakturePozycjaCommand
                {
                    IdKategoriiKosztu = pozycja.IdKategoriiKosztu,
                    Opis = pozycja.Opis,
                    KwotaNetto = pozycja.KwotaNetto,
                    KwotaBrutto = pozycja.KwotaBrutto,
                    Alokacje = pozycja.Alokacje.Select(alokacja => new WystawFaktureAlokacjaCommand
                    {
                        IdEncji = alokacja.IdEncji,
                        KwotaNetto = alokacja.KwotaNetto,
                        KwotaBrutto = alokacja.KwotaBrutto
                    }).ToList()
                }).ToList()
            }, ct);

            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}