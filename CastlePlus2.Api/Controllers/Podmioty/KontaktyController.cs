using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Podmioty.Kontakty.Commands.CreateKontakt;
using CastlePlus2.Application.Podmioty.Kontakty.Commands.UpdateKontakt;
using CastlePlus2.Application.Podmioty.Kontakty.Commands.DeleteKontakt;
using CastlePlus2.Application.Podmioty.Kontakty.Queries.GetKontaktById;
using CastlePlus2.Application.Podmioty.Kontakty.Queries.GetKontaktyByPodmiotId;
using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Contracts.Requests.Podmioty;
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
        public async Task<IActionResult> Create([FromBody] CreateKontaktRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new CreateKontaktCommand
            {
                IdPodmiotu = request.IdPodmiotu,
                Rodzaj = request.Rodzaj,
                Wartosc = request.Wartosc
            }, ct);
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
        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(KontaktDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateKontaktRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new UpdateKontaktCommand { IdKontaktu = id, Request = request }, ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeleteKontaktCommand(id), ct);
            return ok ? NoContent() : NotFound();
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
