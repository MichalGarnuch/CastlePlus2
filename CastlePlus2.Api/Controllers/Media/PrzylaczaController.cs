using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Media.Przylacza.Commands.CreatePrzylacze;
using CastlePlus2.Application.Media.Przylacza.Queries.GetPrzylaczeById;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Media
{
    [ApiController]
    [Route("api/media/[controller]")]
    public class PrzylaczaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PrzylaczaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PrzylaczeDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreatePrzylaczeCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdPrzylacza }, result);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(PrzylaczeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetPrzylaczeByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
