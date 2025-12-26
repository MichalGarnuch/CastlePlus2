// PLIK: CastlePlus2.Api/Controllers/Dokumenty/PowiazaniaDokumentuController.cs
using CastlePlus2.Application.Dokumenty.PowiazaniaDokumentu.Commands.CreatePowiazanieDokumentu;
using CastlePlus2.Application.Dokumenty.PowiazaniaDokumentu.Commands.DeletePowiazanieDokumentu;
using CastlePlus2.Application.Dokumenty.PowiazaniaDokumentu.Commands.UpdatePowiazanieDokumentu;
using CastlePlus2.Application.Dokumenty.PowiazaniaDokumentu.Queries.GetAllPowiazaniaDokumentu;
using CastlePlus2.Application.Dokumenty.PowiazaniaDokumentu.Queries.GetPowiazanieDokumentuById;
using CastlePlus2.Contracts.DTOs.Dokumenty;
using CastlePlus2.Contracts.Requests.Dokumenty;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Dokumenty
{
    [ApiController]
    [Route("api/dokumenty/[controller]")]
    public class PowiazaniaDokumentuController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PowiazaniaDokumentuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<PowiazanieDokumentuDto>>> GetAll(CancellationToken ct)
        {
            var list = await _mediator.Send(new GetAllPowiazaniaDokumentuQuery(), ct);
            return Ok(list);
        }

        [HttpGet("{idPowiazania:long}")]
        public async Task<ActionResult<PowiazanieDokumentuDto>> GetById(long idPowiazania, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetPowiazanieDokumentuByIdQuery(idPowiazania), ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PowiazanieDokumentuDto>> Create([FromBody] CreatePowiazanieDokumentuRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new CreatePowiazanieDokumentuCommand
            {
                IdDokumentu = request.IdDokumentu,
                IdEncji = request.IdEncji
            }, ct);

            return CreatedAtAction(nameof(GetById), new { idPowiazania = result.IdPowiazania }, result);
        }

        [HttpPut("{idPowiazania:long}")]
        public async Task<IActionResult> Update(long idPowiazania, [FromBody] UpdatePowiazanieDokumentuRequest request, CancellationToken ct)
        {
            var ok = await _mediator.Send(new UpdatePowiazanieDokumentuCommand
            {
                IdPowiazania = idPowiazania,
                IdDokumentu = request.IdDokumentu,
                IdEncji = request.IdEncji
            }, ct);

            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{idPowiazania:long}")]
        public async Task<IActionResult> Delete(long idPowiazania, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeletePowiazanieDokumentuCommand(idPowiazania), ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
