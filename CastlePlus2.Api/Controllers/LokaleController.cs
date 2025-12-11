using System;
using System.Threading.Tasks;
using CastlePlus2.Application.Rdzen.Lokale.Commands.CreateLokal;
using CastlePlus2.Application.Rdzen.Lokale.Queries.GetLokalById;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers
{
    /// <summary>
    /// Kontroler REST dla zasobu Lokale.
    /// Ścieżka bazowa: api/Lokale
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LokaleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LokaleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Tworzy nowy lokal w istniejącym budynku.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(LokalDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LokalDto>> Post([FromBody] CreateLokalCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }

        /// <summary>
        /// Zwraca lokal po Id (IdEncji).
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(LokalDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LokalDto>> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetLokalByIdQuery(id));

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
