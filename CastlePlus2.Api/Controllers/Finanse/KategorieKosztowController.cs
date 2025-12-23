using CastlePlus2.Application.Finanse.KategorieKosztow.Commands.CreateKategoriaKosztu;
using CastlePlus2.Application.Finanse.KategorieKosztow.Commands.DeleteKategoriaKosztu;
using CastlePlus2.Application.Finanse.KategorieKosztow.Commands.UpdateKategoriaKosztu;
using CastlePlus2.Application.Finanse.KategorieKosztow.Queries.GetAllKategorieKosztow;
using CastlePlus2.Application.Finanse.KategorieKosztow.Queries.GetKategoriaKosztuById;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Finanse
{
    [ApiController]
    [Route("api/finanse/[controller]")]
    public class KategorieKosztowController : ControllerBase
    {
        private readonly IMediator _mediator;

        public KategorieKosztowController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<KategoriaKosztuDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllKategorieKosztowQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(KategoriaKosztuDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetKategoriaKosztuByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(KategoriaKosztuDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateKategoriaKosztuRequest request, CancellationToken ct)
        {
            var cmd = new CreateKategoriaKosztuCommand
            {
                Kod = (request.Kod ?? string.Empty).Trim(),
                Nazwa = (request.Nazwa ?? string.Empty).Trim()
            };

            var result = await _mediator.Send(cmd, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdKategoriiKosztu }, result);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(KategoriaKosztuDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateKategoriaKosztuRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new UpdateKategoriaKosztuCommand
            {
                IdKategoriiKosztu = id,
                Kod = (request.Kod ?? string.Empty).Trim(),
                Nazwa = (request.Nazwa ?? string.Empty).Trim()
            }, ct);

            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken ct)
        {
            await _mediator.Send(new DeleteKategoriaKosztuCommand { IdKategoriiKosztu = id }, ct);
            return NoContent();
        }
    }
}
