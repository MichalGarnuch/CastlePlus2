using CastlePlus2.Application.Rdzen.Nieruchomosci.Commands.CreateNieruchomosc;
using CastlePlus2.Application.Rdzen.Nieruchomosci.Commands.DeleteNieruchomosc;
using CastlePlus2.Application.Rdzen.Nieruchomosci.Commands.UpdateNieruchomosc;
using CastlePlus2.Application.Rdzen.Nieruchomosci.Queries.GetAllNieruchomosci;
using CastlePlus2.Application.Rdzen.Nieruchomosci.Queries.GetNieruchomoscById;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Rzden
{
    [ApiController]
    [Route("api/[controller]")]
    public class NieruchomosciController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NieruchomosciController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<NieruchomoscDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<NieruchomoscDto>>> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllNieruchomosciQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(NieruchomoscDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NieruchomoscDto>> GetById(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetNieruchomoscByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(NieruchomoscDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<NieruchomoscDto>> Create([FromBody] CreateNieruchomoscRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new CreateNieruchomoscCommand { Request = request }, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(NieruchomoscDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NieruchomoscDto>> Update(Guid id, [FromBody] UpdateNieruchomoscRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new UpdateNieruchomoscCommand(id, request), ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeleteNieruchomoscCommand(id), ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
