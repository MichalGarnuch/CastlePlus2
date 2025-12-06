using CastlePlus2.Application.Nieruchomosci.Commands.CreateNieruchomosc;
using CastlePlus2.Application.Nieruchomosci.Queries.GetNieruchomoscById;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers
{
    /// <summary>
    /// Kontroler REST API dla zasobu: Nieruchomości.
    /// Ścieżka bazowa: api/nieruchomosci
    /// </summary>
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
        /// Pobiera szczegóły nieruchomości po ID.
        /// GET: api/nieruchomosci/{id}
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(NieruchomoscDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NieruchomoscDto>> GetById(Guid id)
        {
            var query = new GetNieruchomoscByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Tworzy nową nieruchomość.
        /// POST: api/nieruchomosci
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNieruchomoscCommand command)
        {
            // MediatR wysyła komendę do CreateNieruchomoscCommandHandler
            var id = await _mediator.Send(command);

            // Zwracamy kod 201 Created oraz nagłówek Location z linkiem do nowej nieruchomości
            return CreatedAtAction(nameof(GetById), new { id = id }, id);
        }
    }
}