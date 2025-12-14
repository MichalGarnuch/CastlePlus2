using CastlePlus2.Application.Najem.UmowyNajmu.Commands.CreateUmowaNajmu;
using CastlePlus2.Application.Najem.UmowyNajmu.Queries.GetAllUmowyNajmu;
using CastlePlus2.Application.Najem.UmowyNajmu.Queries.GetUmowaNajmuById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Najem
{
    [ApiController]
    [Route("api/najem/[controller]")]
    public class UmowyNajmuController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UmowyNajmuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUmowaNajmuCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllUmowyNajmuQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetUmowaNajmuByIdQuery(id), ct);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
