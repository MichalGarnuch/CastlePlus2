using CastlePlus2.Application.Utrzymanie.Zgloszenia.Commands.ZglosUsterke;
using CastlePlus2.Application.Utrzymanie.Zgloszenia.Queries.GetZglosUsterkeContext;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using CastlePlus2.Contracts.Requests.Utrzymanie;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Utrzymanie
{
    [ApiController]
    [Route("api/utrzymanie/zgloszenia")]
    public class ZgloszeniaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ZgloszeniaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("context")]
        [ProducesResponseType(typeof(ZglosUsterkeContextDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContext(CancellationToken ct)
        {
            var context = await _mediator.Send(new GetZglosUsterkeContextQuery(), ct);
            return Ok(context);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ZglosUsterkeResult), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ZglosUsterke([FromBody] ZglosUsterkeRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new ZglosUsterkeCommand
            {
                IdEncjiGospodarza = request.IdEncjiGospodarza,
                Tytul = request.Tytul,
                Opis = request.Opis
            }, ct);

            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}