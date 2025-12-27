using CastlePlus2.Application.Rdzen.Adresy.Commands.CreateAdres;
using CastlePlus2.Application.Rdzen.Adresy.Commands.DeleteAdres;
using CastlePlus2.Application.Rdzen.Adresy.Commands.UpdateAdres;
using CastlePlus2.Application.Rdzen.Adresy.Queries.GetAllAdresy;
using CastlePlus2.Application.Adresy.Queries.GetAdresById;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;
using Microsoft.AspNetCore.Http;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Rdzen
{
    [ApiController]
    [Route("api/rdzen/[controller]")]
    public class AdresyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdresyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<AdresDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AdresDto>>> GetAll(CancellationToken ct)
        {
            var list = await _mediator.Send(new GetAllAdresyQuery(), ct);
            return Ok(list);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(AdresDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AdresDto>> GetById(long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAdresByIdQuery(id), ct);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AdresDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateAdresRequest request, CancellationToken ct)
        {
            var cmd = new CreateAdresCommand
            {
                Kraj = request.Kraj,
                Wojewodztwo = request.Wojewodztwo,
                Powiat = request.Powiat,
                Gmina = request.Gmina,
                Miejscowosc = request.Miejscowosc,
                Ulica = request.Ulica,
                Numer = request.Numer,
                KodPocztowy = request.KodPocztowy,
                AdresPelny = request.AdresPelny
            };

            var created = await _mediator.Send(cmd, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.IdAdresu }, created);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateAdresRequest request, CancellationToken ct)
        {
            var ok = await _mediator.Send(new UpdateAdresCommand
            {
                IdAdresu = id,
                Kraj = request.Kraj,
                Wojewodztwo = request.Wojewodztwo,
                Powiat = request.Powiat,
                Gmina = request.Gmina,
                Miejscowosc = request.Miejscowosc,
                Ulica = request.Ulica,
                Numer = request.Numer,
                KodPocztowy = request.KodPocztowy,
                AdresPelny = request.AdresPelny
            }, ct);

            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeleteAdresCommand(id), ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
