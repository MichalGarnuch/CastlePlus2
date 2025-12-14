using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Podmioty.Kontakty.Commands.CreateKontakt;
using CastlePlus2.Application.Podmioty.Kontakty.Queries.GetKontaktById;
using CastlePlus2.Application.Podmioty.Kontakty.Queries.GetKontaktyByPodmiotId;
using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Podmioty
{
    [ApiController]
    [Route("api/podmioty/[controller]")]
    public class KontaktyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public KontaktyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(KontaktDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateKontaktCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdKontaktu }, result);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(KontaktDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetKontaktByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        // Ten endpoint jest typowo “pod testy i UI”: łatwo sprawdzisz wszystkie kontakty podmiotu.
        [HttpGet("by-podmiot/{idPodmiotu:long}")]
        [ProducesResponseType(typeof(List<KontaktDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByPodmiot([FromRoute] long idPodmiotu, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetKontaktyByPodmiotIdQuery(idPodmiotu), ct);
            return Ok(result);
        }
    }
}
