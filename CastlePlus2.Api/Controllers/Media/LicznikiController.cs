using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Media.Liczniki.Commands.CreateLicznik;
using CastlePlus2.Application.Media.Liczniki.Commands.DeleteLicznik;
using CastlePlus2.Application.Media.Liczniki.Commands.UpdateLicznik;
using CastlePlus2.Application.Media.Liczniki.Queries.GetAllLiczniki;
using CastlePlus2.Application.Media.Liczniki.Queries.GetLicznikById;
using CastlePlus2.Contracts.DTOs.Media;
using CastlePlus2.Contracts.Requests.Media;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Media
{
    [ApiController]
    [Route("api/media/[controller]")]
    public class LicznikiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LicznikiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<LicznikDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllLicznikiQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(LicznikDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetLicznikByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(LicznikDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateLicznikRequest request, CancellationToken ct)
        {
            var cmd = new CreateLicznikCommand
            {
                IdPrzylacza = request.IdPrzylacza,
                IdLicznikaNadrzednego = request.IdLicznikaNadrzednego,
                NumerNV = request.NumerNV,
                KodJednostki = request.KodJednostki,
                WspolczynnikPrzeliczeniowy = request.WspolczynnikPrzeliczeniowy,
                Aktywny = request.Aktywny
            };

            var result = await _mediator.Send(cmd, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdLicznika }, result);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(LicznikDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateLicznikRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new UpdateLicznikCommand(id, request), ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeleteLicznikCommand(id), ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
