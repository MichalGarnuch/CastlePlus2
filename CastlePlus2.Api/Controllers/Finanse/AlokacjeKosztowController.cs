using CastlePlus2.Application.Finanse.AlokacjeKosztow.Commands.CreateAlokacjaKosztu;
using CastlePlus2.Application.Finanse.AlokacjeKosztow.Queries.GetAlokacjaKosztuById;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Finanse
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlokacjeKosztowController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AlokacjeKosztowController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AlokacjaKosztuDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateAlokacjaKosztuCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdAlokacji }, result);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(AlokacjaKosztuDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAlokacjaKosztuByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
