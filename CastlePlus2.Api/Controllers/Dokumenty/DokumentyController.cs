using CastlePlus2.Application.Dokumenty.Dokumenty.Commands.CreateDokument;
using CastlePlus2.Application.Dokumenty.Dokumenty.Queries.GetDokumentById;
using CastlePlus2.Contracts.DTOs.Dokumenty;
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

        [HttpPost]
        public async Task<ActionResult<DokumentDto>> Create(
            [FromBody] CreateDokumentCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.IdDokumentu }, result);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<DokumentDto>> GetById(
            [FromRoute] long id,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetDokumentByIdQuery(id), cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
