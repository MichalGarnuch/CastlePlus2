using CastlePlus2.Application.Finanse.PozycjeKosztow.Commands.CreatePozycjaKosztu;
using CastlePlus2.Application.Finanse.PozycjeKosztow.Queries.GetPozycjaKosztuById;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Finanse
{
    [ApiController]
    [Route("api/finanse/[controller]")]
    public class PozycjeKosztowController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PozycjeKosztowController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PozycjaKosztuDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreatePozycjaKosztuCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdPozycjiKosztu }, result);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(PozycjaKosztuDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetPozycjaKosztuByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
