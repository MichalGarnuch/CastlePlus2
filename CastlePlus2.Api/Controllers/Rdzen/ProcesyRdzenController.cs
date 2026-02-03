using CastlePlus2.Application.Rdzen.ProcesyRdzen.Commands.PrzypiszAdres;
using CastlePlus2.Application.Rdzen.ProcesyRdzen.Queries.GetPrzypisanieAdresuContext;
using CastlePlus2.Application.Rdzen.ProcesyRdzen.Queries.SearchEncjeLookup;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CastlePlus2.Api.Controllers.Rdzen
{
    [ApiController]
    [Authorize]
    [Route("api/rdzen/procesy")]
    public class ProcesyRdzenController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProcesyRdzenController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("adresy-przypisania/context")]
        [ProducesResponseType(typeof(PrzypisanieAdresuContextDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAdresyPrzypisaniaContext(CancellationToken ct)
        {
            var context = await _mediator.Send(new GetPrzypisanieAdresuContextQuery(), ct);
            return Ok(context);
        }

        [HttpPost("adresy-przypisania/przypisz")]
        [ProducesResponseType(typeof(PrzypiszAdresResultDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PrzypiszAdres([FromBody] PrzypiszAdresRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new PrzypiszAdresCommand(
                request.IdEncji,
                request.IdAdresu,
                request.OdDnia,
                request.DoDnia), ct);

            return StatusCode(StatusCodes.Status201Created, result);
        }
        [HttpGet("adresy-przypisania/encje-lookup")]
        [ProducesResponseType(typeof(List<EncjaLookupDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchEncjeLookup(
            [FromQuery] string? typEncji,
            [FromQuery] string? q,
            [FromQuery] int take = 50,
          CancellationToken ct = default)
        {
            var result = await _mediator.Send(new SearchEncjeLookupQuery(typEncji, q, take), ct);
            return Ok(result);
        }
    }
}