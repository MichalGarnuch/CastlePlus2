using System.Threading.Tasks;
using CastlePlus2.Application.Utrzymanie.ZleceniaPracy.Commands.CreateZleceniePracy;
using CastlePlus2.Application.Utrzymanie.ZleceniaPracy.Queries.GetZleceniePracyById;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Utrzymanie
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZleceniaPracyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ZleceniaPracyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Tworzy nowe zlecenie pracy.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ZleceniePracyDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ZleceniePracyDto>> Create([FromBody] CreateZleceniePracyCommand command)
        {
            var dto = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetById),
                new { id = dto.IdZlecenia },
                dto);
        }

        /// <summary>
        /// Zwraca zlecenie pracy po IdZlecenia (bigint).
        /// </summary>
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(ZleceniePracyDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ZleceniePracyDto>> GetById(long id)
        {
            var dto = await _mediator.Send(new GetZleceniePracyByIdQuery(id));

            if (dto is null)
                return NotFound();

            return Ok(dto);
        }
    }
}
