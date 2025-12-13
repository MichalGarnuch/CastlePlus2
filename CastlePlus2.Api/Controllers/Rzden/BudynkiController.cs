using CastlePlus2.Application.Rdzen.Budynki.Queries.GetBudynekById;
using CastlePlus2.Application.Rdzen.Budynki.Commands.CreateBudynek;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CastlePlus2.Api.Controllers.Rzden
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

        /// <summary>
        /// Pobiera budynek po Id (Guid).
        /// GET: api/Budynki/{id}
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(BudynekDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetBudynekByIdQuery(id));

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Tworzy nowy budynek.
        /// POST: api/Budynki
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(BudynekDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateBudynekCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
    }
}
