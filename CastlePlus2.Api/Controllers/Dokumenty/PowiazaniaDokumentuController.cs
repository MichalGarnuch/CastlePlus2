using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Dokumenty.PowiazaniaDokumentu.Commands.CreatePowiazanieDokumentu;
using CastlePlus2.Application.Dokumenty.PowiazaniaDokumentu.Queries.GetPowiazanieDokumentuById;
using CastlePlus2.Contracts.DTOs.Dokumenty;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PowiazaniaDokumentuController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PowiazaniaDokumentuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PowiazanieDokumentuDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreatePowiazanieDokumentuCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdPowiazania }, result);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(PowiazanieDokumentuDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetPowiazanieDokumentuByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
