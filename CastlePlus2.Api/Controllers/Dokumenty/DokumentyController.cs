// PLIK: CastlePlus2.Api/Controllers/Dokumenty/DokumentyController.cs
using CastlePlus2.Application.Dokumenty.Dokumenty.Commands.CreateDokument;
using CastlePlus2.Application.Dokumenty.Dokumenty.Commands.DeleteDokument;
using CastlePlus2.Application.Dokumenty.Dokumenty.Commands.UpdateDokument;
using CastlePlus2.Application.Dokumenty.Dokumenty.Queries.GetAllDokumenty;
using CastlePlus2.Application.Dokumenty.Dokumenty.Queries.GetDokumentById;
using CastlePlus2.Contracts.DTOs.Dokumenty;
using CastlePlus2.Contracts.Requests.Dokumenty;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DokumentyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DokumentyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<DokumentDto>>> GetAll(CancellationToken cancellationToken)
        {
            var list = await _mediator.Send(new GetAllDokumentyQuery(), cancellationToken);
            return Ok(list);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<DokumentDto>> GetById([FromRoute] long id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetDokumentByIdQuery(id), cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<DokumentDto>> Create([FromBody] CreateDokumentRequest request, CancellationToken cancellationToken)
        {
            var cmd = new CreateDokumentCommand
            {
                IdEncjiOwner = request.IdEncjiOwner,
                Nazwa = request.Nazwa,
                Opis = request.Opis,
                SciezkaPliku = request.SciezkaPliku
            };

            var result = await _mediator.Send(cmd, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.IdDokumentu }, result);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateDokumentRequest request, CancellationToken cancellationToken)
        {
            var ok = await _mediator.Send(new UpdateDokumentCommand
            {
                IdDokumentu = id,
                IdEncjiOwner = request.IdEncjiOwner,
                Nazwa = request.Nazwa,
                Opis = request.Opis,
                SciezkaPliku = request.SciezkaPliku
            }, cancellationToken);

            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken cancellationToken)
        {
            var ok = await _mediator.Send(new DeleteDokumentCommand(id), cancellationToken);
            return ok ? NoContent() : NotFound();
        }
    }
}
