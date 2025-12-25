using CastlePlus2.Application.Najem.UmowyNajmu.Commands.CreateUmowaNajmu;
using CastlePlus2.Application.Najem.UmowyNajmu.Commands.DeleteUmowaNajmu;
using CastlePlus2.Application.Najem.UmowyNajmu.Commands.UpdateUmowaNajmu;
using CastlePlus2.Application.Najem.UmowyNajmu.Queries.GetAllUmowyNajmu;
using CastlePlus2.Application.Najem.UmowyNajmu.Queries.GetUmowaNajmuById;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Najem
{
    [ApiController]
    [Route("api/najem/[controller]")]
    public class UmowyNajmuController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UmowyNajmuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(UmowaNajmuDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateUmowaNajmuRequest request, CancellationToken ct)
        {
            var command = new CreateUmowaNajmuCommand
            {
                IdWynajmujacego = request.IdWynajmujacego,
                IdNajemcy = request.IdNajemcy,
                DataZawarcia = request.DataZawarcia,
                DataPoczatku = request.DataPoczatku,
                DataZakonczenia = request.DataZakonczenia,
                KodWaluty = request.KodWaluty,
                KodIndeksacji = request.KodIndeksacji
            };

            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<UmowaNajmuDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllUmowyNajmuQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(UmowaNajmuDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetUmowaNajmuByIdQuery(id), ct);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(UmowaNajmuDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUmowaNajmuRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new UpdateUmowaNajmuCommand(id, request), ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeleteUmowaNajmuCommand(id), ct);
            return ok ? NoContent() : NotFound();
        }
    }
}