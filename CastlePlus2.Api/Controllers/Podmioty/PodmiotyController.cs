using CastlePlus2.Application.Podmioty.Podmioty.Commands.CreatePodmiot;
using CastlePlus2.Application.Podmioty.Podmioty.Commands.DeletePodmiot;
using CastlePlus2.Application.Podmioty.Podmioty.Commands.UpdatePodmiot;
using CastlePlus2.Application.Podmioty.Podmioty.Queries.GetAllPodmioty;
using CastlePlus2.Application.Podmioty.Podmioty.Queries.GetPodmiotById;
using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Contracts.Requests.Podmioty;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Podmioty
{
    [ApiController]
    [Route("api/podmioty/[controller]")]
    public class PodmiotyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PodmiotyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PodmiotDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreatePodmiotRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new CreatePodmiotCommand
            {
                Nazwa = request.Nazwa,
                NIP = request.NIP,
                REGON = request.REGON,
                PESEL = request.PESEL,
                TypPodmiotu = request.TypPodmiotu
            }, ct);

            return CreatedAtAction(nameof(GetById), new { id = result.IdPodmiotu }, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PodmiotDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllPodmiotyQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(PodmiotDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetPodmiotByIdQuery { IdPodmiotu = id }, ct);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(PodmiotDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdatePodmiotRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new UpdatePodmiotCommand { IdPodmiotu = id, Request = request }, ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeletePodmiotCommand(id), ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
