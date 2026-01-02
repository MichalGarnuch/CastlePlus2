using CastlePlus2.Application.Dokumenty.Rejestracja.Commands.RegisterDokument;
using CastlePlus2.Application.Dokumenty.Rejestracja.Queries.GetRegisterDokumentContext;
using CastlePlus2.Contracts.DTOs.Dokumenty;
using CastlePlus2.Contracts.Requests.Dokumenty;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Dokumenty
{
    [ApiController]
    [Route("api/dokumenty/procesy/rejestracja")]
    public class ProcesyDokumentowController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProcesyDokumentowController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("context")]
        [ProducesResponseType(typeof(RegisterDokumentContextDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContext(CancellationToken ct)
        {
            var context = await _mediator.Send(new GetRegisterDokumentContextQuery(), ct);
            return Ok(context);
        }

        [HttpPost]
        [ProducesResponseType(typeof(RegisterDokumentResultDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterDokumentRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new RegisterDokumentCommand
            {
                IdEncji = request.IdEncji,
                Nazwa = request.Nazwa,
                Opis = request.Opis,
                SciezkaPliku = request.SciezkaPliku
            }, ct);

            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}