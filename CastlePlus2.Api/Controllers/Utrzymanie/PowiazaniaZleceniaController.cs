using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Utrzymanie.PowiazaniaZlecenia.Commands.CreatePowiazanieZlecenia;
using CastlePlus2.Application.Utrzymanie.PowiazaniaZlecenia.Queries.GetPowiazanieZleceniaById;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using MediatR;
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

        [HttpPost]
        [ProducesResponseType(typeof(PowiazanieZleceniaDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreatePowiazanieZleceniaCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdPowiazania }, result);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(PowiazanieZleceniaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetPowiazanieZleceniaByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
