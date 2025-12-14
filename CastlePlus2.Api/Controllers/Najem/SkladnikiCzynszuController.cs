using CastlePlus2.Application.Najem.SkladnikiCzynszu.Commands.CreateSkladnikCzynszu;
using CastlePlus2.Application.Najem.SkladnikiCzynszu.Queries.GetAllSkladnikiCzynszu;
using CastlePlus2.Application.Najem.SkladnikiCzynszu.Queries.GetSkladnikCzynszuById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Najem
{
    [ApiController]
    [Route("api/najem/[controller]")]
    public class SkladnikiCzynszuController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SkladnikiCzynszuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSkladnikCzynszuCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdSkladnikaCzynszu }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllSkladnikiCzynszuQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetSkladnikCzynszuByIdQuery(id), ct);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
