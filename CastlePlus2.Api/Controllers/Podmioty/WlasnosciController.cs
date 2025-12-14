using System;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Podmioty.Wlasnosci.Commands.CreateWlasnosc;
using CastlePlus2.Application.Podmioty.Wlasnosci.Queries.GetWlasnoscById;
using CastlePlus2.Application.Podmioty.Wlasnosci.Queries.GetWlasnosciByEncjaId;
using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Podmioty
{
    [ApiController]
    [Route("api/podmioty/[controller]")]
    public class WlasnosciController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WlasnosciController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Tworzy wpis własności (Podmiot -> Encja) z udziałem i zakresem dat.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(WlasnoscDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateWlasnoscRequest request, CancellationToken ct)
        {
            // Swagger: wklejasz request, klikasz Execute, a w odpowiedzi dostajesz m.in. IdWlasnosci
            var result = await _mediator.Send(new CreateWlasnoscCommand { Request = request }, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdWlasnosci }, result);
        }

        /// <summary>
        /// Pobiera pojedynczy wpis własności po IdWlasnosci.
        /// </summary>
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(WlasnoscDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetWlasnoscByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Lista własności dla danej Encji (super do weryfikacji, że FK + zapis działa).
        /// </summary>
        [HttpGet("by-encja/{idEncji:guid}")]
        [ProducesResponseType(typeof(WlasnoscDto[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByEncja([FromRoute] Guid idEncji, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetWlasnosciByEncjaIdQuery(idEncji), ct);
            return Ok(result);
        }
    }
}
