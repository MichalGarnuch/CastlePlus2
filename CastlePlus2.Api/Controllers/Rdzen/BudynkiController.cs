using CastlePlus2.Application.Rdzen.Budynki.Commands.CreateBudynek;
using CastlePlus2.Application.Rdzen.Budynki.Commands.DeleteBudynek;
using CastlePlus2.Application.Rdzen.Budynki.Commands.UpdateBudynek;
using CastlePlus2.Application.Rdzen.Budynki.Queries.GetAllBudynki;
using CastlePlus2.Application.Rdzen.Budynki.Queries.GetBudynekById;
using CastlePlus2.Application.Rdzen.Budynki.Queries.GetBudynkiByNieruchomoscId;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Rdzen
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudynkiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BudynkiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<BudynekDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<BudynekDto>>> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllBudynkiQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(BudynekDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetBudynekByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BudynekDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateBudynekRequest request, CancellationToken ct)
        {
            if (request is null) return BadRequest();

            var command = new CreateBudynekCommand
            {
                IdNieruchomosci = request.IdNieruchomosci,
                KodBudynku = request.KodBudynku,
                IdAdresu = request.IdAdresu,
                Kondygnacje = request.Kondygnacje,
                PowierzchniaUzytkowa = request.PowierzchniaUzytkowa
            };

            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateBudynekRequest request, CancellationToken ct)
        {
            if (request is null) return BadRequest();

            var ok = await _mediator.Send(new UpdateBudynekCommand
            {
                Id = id,
                IdNieruchomosci = request.IdNieruchomosci,
                KodBudynku = request.KodBudynku,
                IdAdresu = request.IdAdresu,
                Kondygnacje = request.Kondygnacje,
                PowierzchniaUzytkowa = request.PowierzchniaUzytkowa
            }, ct);

            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeleteBudynekCommand(id), ct);
            return ok ? NoContent() : NotFound();
        }
        [HttpGet("by-nieruchomosc/{idNieruchomosci:guid}")]
        public async Task<ActionResult<List<BudynekDto>>> GetByNieruchomosc(Guid idNieruchomosci, CancellationToken ct)
        {
            var list = await _mediator.Send(new GetBudynkiByNieruchomoscIdQuery(idNieruchomosci), ct);
            return Ok(list);
        }
    }
}
