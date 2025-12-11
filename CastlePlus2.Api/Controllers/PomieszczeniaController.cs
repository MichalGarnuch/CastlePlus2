using System;
using System.Threading.Tasks;
using CastlePlus2.Application.Rdzen.Pomieszczenia.Commands.CreatePomieszczenie;
using CastlePlus2.Application.Rdzen.Pomieszczenia.Queries.GetPomieszczenieById;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Rdzen
{
    [ApiController]
    [Route("api/[controller]")]
    public class PomieszczeniaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PomieszczeniaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Tworzy nowe pomieszczenie w lokalu.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(PomieszczenieDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PomieszczenieDto>> CreatePomieszczenie(
            [FromBody] CreatePomieszczenieCommand command)
        {
            var dto = await _mediator.Send(command);

            // Zwracamy 201 Created z lokalizacją zasobu
            return CreatedAtAction(
                nameof(GetPomieszczenieById),
                new { id = dto.Id },
                dto);
        }

        /// <summary>
        /// Zwraca pojedyncze pomieszczenie po Id.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PomieszczenieDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PomieszczenieDto>> GetPomieszczenieById(Guid id)
        {
            var dto = await _mediator.Send(new GetPomieszczenieByIdQuery { Id = id });

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }
    }
}
