using CastlePlus2.Application.Podmioty.Podmioty.Commands.CreatePodmiot;
using CastlePlus2.Application.Podmioty.Podmioty.Queries.GetAllPodmioty;
using CastlePlus2.Application.Podmioty.Podmioty.Queries.GetPodmiotById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Podmioty
{
    [ApiController]
    [Route("api/podmioty/[controller]")]
    public class PodmiotyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PodmiotyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePodmiotCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdPodmiotu }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllPodmiotyQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetPodmiotByIdQuery { IdPodmiotu = id }, ct);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
