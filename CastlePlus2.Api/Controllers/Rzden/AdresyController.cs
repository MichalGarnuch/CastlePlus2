using CastlePlus2.Application.Rdzen.Adresy.Commands.CreateAdres;
using CastlePlus2.Application.Rdzen.Adresy.Commands.DeleteAdres;
using CastlePlus2.Application.Rdzen.Adresy.Commands.UpdateAdres;
using CastlePlus2.Application.Rdzen.Adresy.Queries.GetAllAdresy;
using CastlePlus2.Application.Adresy.Queries.GetAdresById;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Contracts.Requests.Rdzen;
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
        public async Task<ActionResult<List<AdresDto>>> GetAll(CancellationToken ct)
        {
            var list = await _mediator.Send(new GetAllAdresyQuery(), ct);
            return Ok(list);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<AdresDto>> GetById(long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAdresByIdQuery(id), ct);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create([FromBody] CreateAdresRequest request, CancellationToken ct)
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

            var id = await _mediator.Send(cmd, ct);

            // Minimalny standard: zwracamy 201 + lokalizację
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id:long}")]
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
        public async Task<IActionResult> Delete(long id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeleteAdresCommand(id), ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
