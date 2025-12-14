using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Media.Liczniki.Commands.CreateLicznik;
using CastlePlus2.Application.Media.Liczniki.Queries.GetLicznikById;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Media
{
    [ApiController]
    [Route("api/[controller]")]
    public class LicznikiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LicznikiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(LicznikDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateLicznikCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdLicznika }, result);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(LicznikDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetLicznikByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
