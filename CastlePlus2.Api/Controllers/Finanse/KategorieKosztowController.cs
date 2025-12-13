using CastlePlus2.Application.Finanse.KategorieKosztow.Commands.CreateKategoriaKosztu;
using CastlePlus2.Application.Finanse.KategorieKosztow.Queries.GetKategoriaKosztuById;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Finanse
{
    [ApiController]
    [Route("api/finanse/[controller]")]
    public class KategorieKosztowController : ControllerBase
    {
        private readonly IMediator _mediator;

        public KategorieKosztowController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(KategoriaKosztuDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateKategoriaKosztuCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdKategoriiKosztu }, result);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(KategoriaKosztuDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetKategoriaKosztuByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
