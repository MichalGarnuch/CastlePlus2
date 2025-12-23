using CastlePlus2.Application.Finanse.AlokacjeKosztow.Commands.CreateAlokacjaKosztu;
using CastlePlus2.Application.Finanse.AlokacjeKosztow.Commands.DeleteAlokacjaKosztu;
using CastlePlus2.Application.Finanse.AlokacjeKosztow.Commands.UpdateAlokacjaKosztu;
using CastlePlus2.Application.Finanse.AlokacjeKosztow.Queries.GetAlokacjaKosztuById;
using CastlePlus2.Application.Finanse.AlokacjeKosztow.Queries.GetAllAlokacjeKosztow;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Finanse
{
    [ApiController]
    [Route("api/finanse/[controller]")]
    public class AlokacjeKosztowController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AlokacjeKosztowController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<AlokacjaKosztuDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var list = await _mediator.Send(new GetAllAlokacjeKosztowQuery(), ct);
            return Ok(list);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(AlokacjaKosztuDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAlokacjaKosztuByIdQuery(id), ct);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AlokacjaKosztuDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateAlokacjaKosztuRequest request, CancellationToken ct)
        {
            var cmd = new CreateAlokacjaKosztuCommand
            {
                IdPozycjiKosztu = request.IdPozycjiKosztu,
                IdEncji = request.IdEncji,
                KwotaNetto = request.KwotaNetto,
                KwotaBrutto = request.KwotaBrutto
            };

            var result = await _mediator.Send(cmd, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdAlokacji }, result);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateAlokacjaKosztuRequest request, CancellationToken ct)
        {
            var ok = await _mediator.Send(new UpdateAlokacjaKosztuCommand
            {
                IdAlokacji = id,
                IdPozycjiKosztu = request.IdPozycjiKosztu,
                IdEncji = request.IdEncji,
                KwotaNetto = request.KwotaNetto,
                KwotaBrutto = request.KwotaBrutto
            }, ct);

            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeleteAlokacjaKosztuCommand(id), ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
