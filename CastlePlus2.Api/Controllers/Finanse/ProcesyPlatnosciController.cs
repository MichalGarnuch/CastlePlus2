using System.Collections.Generic;
using System.Linq;
using CastlePlus2.Application.Finanse.ProcesyPlatnosci.Commands.ZarejestrujPlatnosc;
using CastlePlus2.Application.Finanse.ProcesyPlatnosci.Queries.GetPlatnoscContext;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CastlePlus2.Api.Controllers.Finanse
{
    [ApiController]
    [Authorize]
    [Route("api/finanse/procesy/platnosci")]
    public class ProcesyPlatnosciController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProcesyPlatnosciController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("context")]
        [ProducesResponseType(typeof(PlatnoscContextDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContext(CancellationToken ct)
        {
            var context = await _mediator.Send(new GetPlatnoscContextQuery(), ct);
            return Ok(context);
        }

        [HttpPost("zarejestruj")]
        [ProducesResponseType(typeof(ZarejestrujPlatnoscResultDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Zarejestruj([FromBody] ZarejestrujPlatnoscRequest request, CancellationToken ct)
        {
            var rozliczenia = request.Rozliczenia ?? new List<ZarejestrujPlatnoscRozliczenieRequest>();

            var result = await _mediator.Send(new ZarejestrujPlatnoscCommand
            {
                IdPodmiotu = request.IdPodmiotu,
                DataPlatnosci = request.DataPlatnosci,
                KodWaluty = request.KodWaluty,
                Kwota = request.Kwota,
                Rozliczenia = rozliczenia.Select(r => new ZarejestrujPlatnoscRozliczenieCommand
                {
                    IdFaktury = r.IdFaktury,
                    Kwota = r.Kwota
                }).ToList()
            }, ct);

            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}