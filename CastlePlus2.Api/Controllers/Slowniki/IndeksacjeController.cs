using CastlePlus2.Application.Slowniki.Indeksacje.Commands.CreateIndeksacja;
using CastlePlus2.Application.Slowniki.Indeksacje.Queries.GetAllIndeksacje;
using CastlePlus2.Application.Slowniki.Indeksacje.Queries.GetIndeksacjaByKod;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Slowniki
{
    [ApiController]
    [Route("api/slowniki/[controller]")]
    public class IndeksacjeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IndeksacjeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateIndeksacjaCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetByKod), new { kod = result.KodIndeksacji }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllIndeksacjeQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{kod}")]
        public async Task<IActionResult> GetByKod([FromRoute] string kod, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetIndeksacjaByKodQuery { KodIndeksacji = kod }, ct);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
