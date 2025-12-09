using System;
using System.Threading.Tasks;
using CastlePlus2.Application.Rdzen.Nieruchomosci.Commands.CreateNieruchomosc;
using CastlePlus2.Application.Rdzen.Nieruchomosci.Queries.GetNieruchomoscById;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers
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

        /// <summary>
        /// Tworzy nową nieruchomość.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(NieruchomoscDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<NieruchomoscDto>> Post([FromBody] CreateNieruchomoscCommand command)
        {
            var result = await _mediator.Send(command);

            // Zwracamy 201 Created + Location do GET /api/Nieruchomosci/{id}
            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }

        /// <summary>
        /// Zwraca nieruchomość po IdEncji.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(NieruchomoscDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NieruchomoscDto>> GetById(Guid id)
        {
            var query = new GetNieruchomoscByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result is null)
                return NotFound();

            return Ok(result);
        }
    }
}
