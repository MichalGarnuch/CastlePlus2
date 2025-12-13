using CastlePlus2.Application.Finanse.Platnosci.Commands.CreatePlatnosc;
using CastlePlus2.Application.Finanse.Platnosci.Queries.GetPlatnoscById;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Finanse
{
    [ApiController]
    [Route("api/finanse/[controller]")]
    public class PlatnosciController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlatnosciController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PlatnoscDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreatePlatnoscCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdPlatnosci }, result);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(PlatnoscDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetPlatnoscByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
