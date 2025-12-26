using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Utrzymanie.PowiazaniaZlecenia.Commands.CreatePowiazanieZlecenia;
using CastlePlus2.Application.Utrzymanie.PowiazaniaZlecenia.Commands.DeletePowiazanieZlecenia;
using CastlePlus2.Application.Utrzymanie.PowiazaniaZlecenia.Commands.UpdatePowiazanieZlecenia;
using CastlePlus2.Application.Utrzymanie.PowiazaniaZlecenia.Queries.GetPowiazaniaZlecenia;
using CastlePlus2.Application.Utrzymanie.PowiazaniaZlecenia.Queries.GetPowiazanieZleceniaById;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using CastlePlus2.Contracts.Requests.Utrzymanie;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Utrzymanie
{
    [ApiController]
    [Route("api/[controller]")]
    public class PowiazaniaZleceniaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PowiazaniaZleceniaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PowiazanieZleceniaDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetPowiazaniaZleceniaQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(PowiazanieZleceniaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetPowiazanieZleceniaByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PowiazanieZleceniaDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreatePowiazanieZleceniaRequest request, CancellationToken ct)
        {
            if (request is null) return BadRequest();

            var command = new CreatePowiazanieZleceniaCommand
            {
                IdZlecenia = request.IdZlecenia,
                IdEncji = request.IdEncji
            };

            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdPowiazania }, result);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(PowiazanieZleceniaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdatePowiazanieZleceniaRequest request, CancellationToken ct)
        {
            if (request is null) return BadRequest();

            var result = await _mediator.Send(new UpdatePowiazanieZleceniaCommand(id, request), ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeletePowiazanieZleceniaCommand(id), ct);
            return ok ? NoContent() : NotFound();
        }
    }
}