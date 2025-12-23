using CastlePlus2.Application.Najem.PrzedmiotyNajmu.Commands.CreatePrzedmiotNajmu;
using CastlePlus2.Application.Najem.PrzedmiotyNajmu.Commands.DeletePrzedmiotNajmu;
using CastlePlus2.Application.Najem.PrzedmiotyNajmu.Commands.UpdatePrzedmiotNajmu;
using CastlePlus2.Application.Najem.PrzedmiotyNajmu.Queries.GetAllPrzedmiotyNajmu;
using CastlePlus2.Application.Najem.PrzedmiotyNajmu.Queries.GetPrzedmiotNajmuById;
using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Najem
{
    [ApiController]
    [Route("api/najem/[controller]")]
    public class PrzedmiotyNajmuController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PrzedmiotyNajmuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PrzedmiotNajmuDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreatePrzedmiotNajmuRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new CreatePrzedmiotNajmuCommand { Request = request }, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdPrzedmiotuNajmu }, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PrzedmiotNajmuDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllPrzedmiotyNajmuQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetPrzedmiotNajmuByIdQuery(id), ct);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(PrzedmiotNajmuDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdatePrzedmiotNajmuRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new UpdatePrzedmiotNajmuCommand
            {
                IdPrzedmiotuNajmu = id,
                Request = request
            }, ct);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeletePrzedmiotNajmuCommand(id), ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
