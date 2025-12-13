using CastlePlus2.Application.Finanse.RozliczeniaPlatnosci.Commands.CreateRozliczeniePlatnosci;
using CastlePlus2.Application.Finanse.RozliczeniaPlatnosci.Queries.GetRozliczeniePlatnosciById;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Finanse
{
    [ApiController]
    [Route("api/finanse/[controller]")]
    public class RozliczeniaPlatnosciController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RozliczeniaPlatnosciController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(RozliczeniePlatnosciDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateRozliczeniePlatnosciCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdRozliczenia }, result);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(RozliczeniePlatnosciDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetRozliczeniePlatnosciByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
