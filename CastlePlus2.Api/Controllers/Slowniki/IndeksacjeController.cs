using CastlePlus2.Application.Slowniki.Indeksacje.Commands.CreateIndeksacja;
using CastlePlus2.Application.Slowniki.Indeksacje.Commands.DeleteIndeksacja;
using CastlePlus2.Application.Slowniki.Indeksacje.Commands.UpdateIndeksacja;
using CastlePlus2.Application.Slowniki.Indeksacje.Queries.GetAllIndeksacje;
using CastlePlus2.Application.Slowniki.Indeksacje.Queries.GetIndeksacjaByKod;
using CastlePlus2.Contracts.Requests.Slownik;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateIndeksacjaRequest request, CancellationToken ct)
        {
            if (request is null) return BadRequest();

            var result = await _mediator.Send(new CreateIndeksacjaCommand
            {
                KodIndeksacji = request.KodIndeksacji,
                Nazwa = request.Nazwa,
                AdresZrodlaURL = request.AdresZrodlaURL
            }, ct);

            return CreatedAtAction(nameof(GetByKod), new { kod = result.KodIndeksacji }, result);
        }

        [HttpPut("{kod}")]
        public async Task<IActionResult> Update([FromRoute] string kod, [FromBody] UpdateIndeksacjaRequest request, CancellationToken ct)
        {
            if (request is null) return BadRequest();

            var ok = await _mediator.Send(new UpdateIndeksacjaCommand
            {
                KodIndeksacji = kod,
                Nazwa = request.Nazwa,
                AdresZrodlaURL = request.AdresZrodlaURL
            }, ct);

            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{kod}")]
        public async Task<IActionResult> Delete([FromRoute] string kod, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeleteIndeksacjaCommand { KodIndeksacji = kod }, ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
