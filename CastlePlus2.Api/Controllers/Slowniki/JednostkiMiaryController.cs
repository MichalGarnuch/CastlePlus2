using CastlePlus2.Application.Slowniki.JednostkiMiary.Commands.CreateJednostkaMiary;
using CastlePlus2.Application.Slowniki.JednostkiMiary.Queries.GetAllJednostkiMiary;
using CastlePlus2.Application.Slowniki.JednostkiMiary.Queries.GetJednostkaMiaryByKod;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Slowniki
{
    [ApiController]
    [Route("api/slowniki/[controller]")]
    public class JednostkiMiaryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JednostkiMiaryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJednostkaMiaryCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetByKod), new { kod = result.KodJednostki }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllJednostkiMiaryQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{kod}")]
        public async Task<IActionResult> GetByKod([FromRoute] string kod, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetJednostkaMiaryByKodQuery { KodJednostki = kod }, ct);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
