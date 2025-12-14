using CastlePlus2.Application.Najem.PrzedmiotyNajmu.Commands.CreatePrzedmiotNajmu;
using CastlePlus2.Application.Najem.PrzedmiotyNajmu.Queries.GetAllPrzedmiotyNajmu;
using CastlePlus2.Application.Najem.PrzedmiotyNajmu.Queries.GetPrzedmiotNajmuById;
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
        public async Task<IActionResult> Create([FromBody] CreatePrzedmiotNajmuCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdPrzedmiotuNajmu }, result);
        }

        [HttpGet]
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
    }
}
