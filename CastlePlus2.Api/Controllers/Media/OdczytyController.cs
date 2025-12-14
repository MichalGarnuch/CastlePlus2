using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Media.Odczyty.Commands.CreateOdczyt;
using CastlePlus2.Application.Media.Odczyty.Queries.GetOdczytById;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Media
{
    [ApiController]
    [Route("api/media/[controller]")]
    public class OdczytyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OdczytyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OdczytDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateOdczytCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdOdczytu }, result);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(OdczytDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetOdczytByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
