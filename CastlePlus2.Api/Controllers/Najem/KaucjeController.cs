using CastlePlus2.Application.Najem.Kaucje.Commands.CreateKaucja;
using CastlePlus2.Application.Najem.Kaucje.Queries.GetAllKaucje;
using CastlePlus2.Application.Najem.Kaucje.Queries.GetKaucjaById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Najem
{
    [ApiController]
    [Route("api/najem/[controller]")]
    public class KaucjeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public KaucjeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateKaucjaCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdKaucji }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllKaucjeQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetKaucjaByIdQuery(id), ct);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
