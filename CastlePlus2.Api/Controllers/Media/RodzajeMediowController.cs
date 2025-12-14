using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Media.RodzajeMediow.Commands.CreateRodzajMedium;
using CastlePlus2.Application.Media.RodzajeMediow.Queries.GetRodzajMediumById;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Media
{
    [ApiController]
    [Route("api/[controller]")]
    public class RodzajeMediowController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RodzajeMediowController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(RodzajMediumDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateRodzajMediumCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { kod = result.KodRodzaju }, result);
        }

        [HttpGet("{kod}")]
        [ProducesResponseType(typeof(RodzajMediumDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] string kod, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetRodzajMediumByIdQuery(kod), ct);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
