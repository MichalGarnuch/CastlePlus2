using CastlePlus2.Application.Finanse.ProcesyPlatnosci.Queries.GetPlatnoscContext;
using CastlePlus2.Application.Podmioty.Wlasnosci.Commands.UstawWlasnosc;
using CastlePlus2.Application.Podmioty.Wlasnosci.Queries.GetWlasnoscContext;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Contracts.Requests.Podmioty;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace CastlePlus2.Api.Controllers.Podmioty
{
    [ApiController]
    [Authorize]
    [Route("api/podmioty/procesy/wlasnosc")]
    public class ProcesyPodmiotyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProcesyPodmiotyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("context")]
        [ProducesResponseType(typeof(WlasnoscContextDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContext(CancellationToken ct)
        {
            var context = await _mediator.Send(new GetWlasnoscContextQuery(), ct);
            return Ok(context);
        }

        [HttpPost("ustaw")]
        [ProducesResponseType(typeof(IReadOnlyList<WlasnoscDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Ustaw([FromBody] UstawWlasnoscRequest request, CancellationToken ct)
        {
            var udzialy = request.Udzialy ?? new List<UstawWlasnoscItemRequest>();

            var result = await _mediator.Send(new UstawWlasnoscCommand
            {
                IdEncji = request.IdEncji,
                Udzialy = udzialy.Select(x => new UstawWlasnoscUdzialCommand
                {
                    IdPodmiotu = x.IdPodmiotu,
                    UdzialProcent = x.UdzialProcent,
                    OdDnia = x.OdDnia,
                    DoDnia = x.DoDnia
                }).ToList()
            }, ct);

            return Ok(result);
        }
    }
}