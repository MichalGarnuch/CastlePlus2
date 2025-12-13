using CastlePlus2.Application.Rdzen.PrzypisaniaAdresow.Commands.CreatePrzypisanieAdresu;
using CastlePlus2.Application.Rdzen.PrzypisaniaAdresow.Queries.GetPrzypisanieAdresuById;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers
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

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(PrzypisanieAdresuDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PrzypisanieAdresuDto>> GetById(long id)
        {
            var result = await _mediator.Send(new GetPrzypisanieAdresuByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PrzypisanieAdresuDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<PrzypisanieAdresuDto>> Post([FromBody] CreatePrzypisanieAdresuCommand command)
        {
            var created = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = created.IdPrzypisaniaAdresu }, created);
        }
    }
}
