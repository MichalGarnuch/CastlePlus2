using CastlePlus2.Application.Rdzen.PrzypisaniaAdresow.Commands.CreatePrzypisanieAdresu;
using CastlePlus2.Application.Rdzen.PrzypisaniaAdresow.Commands.DeletePrzypisanieAdresu;
using CastlePlus2.Application.Rdzen.PrzypisaniaAdresow.Commands.UpdatePrzypisanieAdresu;
using CastlePlus2.Application.Rdzen.PrzypisaniaAdresow.Queries.GetAllPrzypisaniaAdresow;
using CastlePlus2.Application.Rdzen.PrzypisaniaAdresow.Queries.GetPrzypisanieAdresuById;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Rzden
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrzypisaniaAdresowController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PrzypisaniaAdresowController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PrzypisanieAdresuDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PrzypisanieAdresuDto>>> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllPrzypisaniaAdresowQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(PrzypisanieAdresuDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PrzypisanieAdresuDto>> GetById(long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetPrzypisanieAdresuByIdQuery(id), ct);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PrzypisanieAdresuDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<PrzypisanieAdresuDto>> Create([FromBody] CreatePrzypisanieAdresuRequest request, CancellationToken ct)
        {
            var created = await _mediator.Send(
                new CreatePrzypisanieAdresuCommand(request.IdEncji, request.IdAdresu, request.OdDnia), ct);

            return CreatedAtAction(nameof(GetById), new { id = created.IdPrzypisaniaAdresu }, created);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(PrzypisanieAdresuDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PrzypisanieAdresuDto>> Update(long id, [FromBody] UpdatePrzypisanieAdresuRequest request, CancellationToken ct)
        {
            var updated = await _mediator.Send(new UpdatePrzypisanieAdresuCommand(id, request), ct);
            if (updated is null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeletePrzypisanieAdresuCommand(id), ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
