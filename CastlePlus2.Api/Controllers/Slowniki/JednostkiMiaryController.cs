using CastlePlus2.Application.Slowniki.JednostkiMiary.Commands.CreateJednostkaMiary;
using CastlePlus2.Application.Slowniki.JednostkiMiary.Commands.DeleteJednostkaMiary;
using CastlePlus2.Application.Slowniki.JednostkiMiary.Commands.UpdateJednostkaMiary;
using CastlePlus2.Application.Slowniki.JednostkiMiary.Queries.GetAllJednostkiMiary;
using CastlePlus2.Application.Slowniki.JednostkiMiary.Queries.GetJednostkaMiaryByKod;
using CastlePlus2.Contracts.DTOs.Slowniki;
using CastlePlus2.Contracts.Requests.Slownik;
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

        [HttpGet]
        public async Task<ActionResult<List<JednostkaMiaryDto>>> GetAll(CancellationToken ct)
        {
            var list = await _mediator.Send(new GetAllJednostkiMiaryQuery(), ct);
            return Ok(list);
        }

        [HttpGet("{kodJednostki}")]
        public async Task<ActionResult<JednostkaMiaryDto>> GetByKod([FromRoute] string kodJednostki, CancellationToken ct)
        {
            var dto = await _mediator.Send(new GetJednostkaMiaryByKodQuery { KodJednostki = kodJednostki }, ct);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<JednostkaMiaryDto>> Create([FromBody] CreateJednostkaMiaryRequest request, CancellationToken ct)
        {
            var dto = await _mediator.Send(new CreateJednostkaMiaryCommand
            {
                KodJednostki = request.KodJednostki,
                Nazwa = request.Nazwa
            }, ct);

            return CreatedAtAction(nameof(GetByKod), new { kodJednostki = dto.KodJednostki }, dto);
        }

        [HttpPut("{kodJednostki}")]
        public async Task<IActionResult> Update([FromRoute] string kodJednostki, [FromBody] UpdateJednostkaMiaryRequest request, CancellationToken ct)
        {
            await _mediator.Send(new UpdateJednostkaMiaryCommand
            {
                KodJednostki = kodJednostki,
                Nazwa = request.Nazwa
            }, ct);

            return NoContent();
        }

        [HttpDelete("{kodJednostki}")]
        public async Task<IActionResult> Delete([FromRoute] string kodJednostki, CancellationToken ct)
        {
            await _mediator.Send(new DeleteJednostkaMiaryCommand { KodJednostki = kodJednostki }, ct);
            return NoContent();
        }
    }
}
