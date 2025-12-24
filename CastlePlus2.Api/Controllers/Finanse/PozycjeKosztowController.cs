using CastlePlus2.Application.Finanse.PozycjeKosztow.Commands.CreatePozycjaKosztu;
using CastlePlus2.Application.Finanse.PozycjeKosztow.Commands.DeletePozycjaKosztu;
using CastlePlus2.Application.Finanse.PozycjeKosztow.Commands.UpdatePozycjaKosztu;
using CastlePlus2.Application.Finanse.PozycjeKosztow.Queries.GetAllPozycjeKosztow;
using CastlePlus2.Application.Finanse.PozycjeKosztow.Queries.GetPozycjaKosztuById;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Finanse
{
    [ApiController]
    [Route("api/finanse/[controller]")]
    public class PozycjeKosztowController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PozycjeKosztowController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PozycjaKosztuDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllPozycjeKosztowQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(PozycjaKosztuDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetPozycjaKosztuByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PozycjaKosztuDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreatePozycjaKosztuRequest request, CancellationToken ct)
        {
            var cmd = new CreatePozycjaKosztuCommand
            {
                IdFaktury = request.IdFaktury,
                IdKategoriiKosztu = request.IdKategoriiKosztu,
                Opis = string.IsNullOrWhiteSpace(request.Opis) ? null : request.Opis.Trim(),
                KwotaNetto = request.KwotaNetto,
                KwotaBrutto = request.KwotaBrutto
            };

            var result = await _mediator.Send(cmd, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdPozycjiKosztu }, result);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(PozycjaKosztuDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdatePozycjaKosztuRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new UpdatePozycjaKosztuCommand
            {
                IdPozycjiKosztu = id,
                IdFaktury = request.IdFaktury,
                IdKategoriiKosztu = request.IdKategoriiKosztu,
                Opis = string.IsNullOrWhiteSpace(request.Opis) ? null : request.Opis.Trim(),
                KwotaNetto = request.KwotaNetto,
                KwotaBrutto = request.KwotaBrutto
            }, ct);

            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken ct)
        {
            await _mediator.Send(new DeletePozycjaKosztuCommand { IdPozycjiKosztu = id }, ct);
            return NoContent();
        }
    }
}
