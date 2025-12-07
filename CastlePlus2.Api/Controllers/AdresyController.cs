using CastlePlus2.Application.Adresy.Queries.GetAdresById;
using CastlePlus2.Application.Rdzen.Adresy.Commands.CreateAdres;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers
{
    /// <summary>
    /// Kontroler REST dla zasobu: Adresy.
    /// Ścieżka bazowa: api/adresy
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AdresyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdresyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Pobiera adres po IdAdresu.
        /// GET: api/adresy/{id}
        /// </summary>
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(AdresDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AdresDto>> GetById(long id)
        {
            var query = new GetAdresByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Tworzy nowy adres.
        /// POST: api/adresy
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
        public async Task<ActionResult<long>> Create([FromBody] CreateAdresCommand command)
        {
            var id = await _mediator.Send(command);

            // Location: api/adresy/{id}
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }
    }
}

