using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Finanse.Platnosci.Commands.CreatePlatnosc;
using CastlePlus2.Application.Finanse.Platnosci.Commands.DeletePlatnosc;
using CastlePlus2.Application.Finanse.Platnosci.Commands.UpdatePlatnosc;
using CastlePlus2.Application.Finanse.Platnosci.Queries.GetAllPlatnosci;
using CastlePlus2.Application.Finanse.Platnosci.Queries.GetPlatnoscById;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Finanse
{
    [ApiController]
    [Route("api/finanse/[controller]")]
    public class PlatnosciController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlatnosciController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PlatnoscDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllPlatnosciQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(PlatnoscDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetPlatnoscByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PlatnoscDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreatePlatnoscRequest request, CancellationToken ct)
        {
            var cmd = new CreatePlatnoscCommand
            {
                IdPodmiotu = request.IdPodmiotu,
                DataPlatnosci = request.DataPlatnosci,
                KodWaluty = request.KodWaluty,
                Kwota = request.Kwota
            };

            var result = await _mediator.Send(cmd, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdPlatnosci }, result);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(PlatnoscDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdatePlatnoscRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new UpdatePlatnoscCommand(id, request), ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeletePlatnoscCommand(id), ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
