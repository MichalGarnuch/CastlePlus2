using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Media.Przylacza.Commands.CreatePrzylacze;
using CastlePlus2.Application.Media.Przylacza.Queries.GetPrzylaczeById;
using CastlePlus2.Application.Media.Przylacza.Commands.DeletePrzylacze;
using CastlePlus2.Application.Media.Przylacza.Commands.UpdatePrzylacze;
using CastlePlus2.Application.Media.Przylacza.Queries.GetAllPrzylacza;
using CastlePlus2.Contracts.Requests.Media;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Media
{
    [ApiController]
    [Route("api/media/[controller]")]
    public class PrzylaczaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PrzylaczaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(PrzylaczeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetPrzylaczeByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<PrzylaczeDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PrzylaczeDto>>> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllPrzylaczaQuery(), ct);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PrzylaczeDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreatePrzylaczeRequest request, CancellationToken ct)
        {
            var command = new CreatePrzylaczeCommand
            {
                IdEncjiGospodarza = request.IdEncjiGospodarza,
                KodRodzaju = request.KodRodzaju,
                Opis = request.Opis
            };

            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdPrzylacza }, result);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(PrzylaczeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdatePrzylaczeRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new UpdatePrzylaczeCommand(id, request), ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeletePrzylaczeCommand(id), ct);
            return ok ? NoContent() : NotFound();
        }

    }
}
