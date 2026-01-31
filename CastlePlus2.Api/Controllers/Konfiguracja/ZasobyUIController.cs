using CastlePlus2.Application.Common.Exceptions;
using CastlePlus2.Application.Konfiguracja.ZasobyUI.Commands.CreateZasobUI;
using CastlePlus2.Application.Konfiguracja.ZasobyUI.Commands.DeleteZasobUI;
using CastlePlus2.Application.Konfiguracja.ZasobyUI.Commands.UpdateZasobUI;
using CastlePlus2.Application.Konfiguracja.ZasobyUI.Queries.GetAllZasobyUI;
using CastlePlus2.Application.Konfiguracja.ZasobyUI.Queries.GetPublicZasobyUI;
using CastlePlus2.Application.Konfiguracja.ZasobyUI.Queries.GetZasobUIById;
using CastlePlus2.Contracts.DTOs.Konfiguracja;
using CastlePlus2.Contracts.Requests.Konfiguracja;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CastlePlus2.Api.Controllers.Konfiguracja
{
    [ApiController]
    [Route("api/konfiguracja/[controller]")]
    public class ZasobyUIController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ZasobyUIController> _logger;

        public ZasobyUIController(IMediator mediator, ILogger<ZasobyUIController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<ZasobUIDto>>> GetAll(
            [FromQuery] string? typ,
            [FromQuery] string? kategoria,
            [FromQuery] bool? aktywny,
            CancellationToken ct)
        {
            var list = await _mediator.Send(new GetAllZasobyUIQuery
            {
                Typ = typ,
                Kategoria = kategoria,
                CzyAktywny = aktywny
            }, ct);

            return Ok(list);
        }

        [HttpGet("{idEncji:guid}")]
        public async Task<ActionResult<ZasobUIDto>> GetById([FromRoute] Guid idEncji, CancellationToken ct)
        {
            var dto = await _mediator.Send(new GetZasobUIByIdQuery(idEncji), ct);
            return dto is null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ZasobUIDto>> Create([FromBody] CreateZasobUIRequest request, CancellationToken ct)
        {
            try
            {
                var dto = await _mediator.Send(new CreateZasobUICommand
                {
                    KodZasobu = request.KodZasobu,
                    Typ = request.Typ,
                    Kategoria = request.Kategoria,
                    CzyAktywny = request.CzyAktywny,
                    Sort = request.Sort,
                    WazneOdUtc = request.WazneOdUtc,
                    WazneDoUtc = request.WazneDoUtc
                }, ct);

                return CreatedAtAction(nameof(GetById), new { idEncji = dto.IdEncji }, dto);
            }
            catch (BusinessConflictException ex)
            {
                _logger.LogWarning(ex, "Konflikt danych przy tworzeniu ZasobUI.");
                return Conflict(BuildProblemDetails(StatusCodes.Status409Conflict, "Konflikt danych", ex.Message));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Błąd bazy danych przy tworzeniu ZasobUI.");
                return Conflict(BuildProblemDetails(StatusCodes.Status409Conflict, "Konflikt danych", ex.Message));
            }
        }

        [HttpPut("{idEncji:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid idEncji, [FromBody] UpdateZasobUIRequest request, CancellationToken ct)
        {
            try
            {
                var ok = await _mediator.Send(new UpdateZasobUICommand
                {
                    IdEncji = idEncji,
                    KodZasobu = request.KodZasobu,
                    Typ = request.Typ,
                    Kategoria = request.Kategoria,
                    CzyAktywny = request.CzyAktywny,
                    Sort = request.Sort,
                    WazneOdUtc = request.WazneOdUtc,
                    WazneDoUtc = request.WazneDoUtc
                }, ct);

                return ok ? NoContent() : NotFound();
            }
            catch (BusinessConflictException ex)
            {
                _logger.LogWarning(ex, "Konflikt danych przy aktualizacji ZasobUI.");
                return Conflict(BuildProblemDetails(StatusCodes.Status409Conflict, "Konflikt danych", ex.Message));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Błąd bazy danych przy aktualizacji ZasobUI.");
                return Conflict(BuildProblemDetails(StatusCodes.Status409Conflict, "Konflikt danych", ex.Message));
            }
        }

        [HttpDelete("{idEncji:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid idEncji, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeleteZasobUICommand(idEncji), ct);
            return ok ? NoContent() : NotFound();
        }

        [HttpGet("public")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ZasobUIPublicDto>>> GetPublic(
            [FromQuery] string typ,
            [FromQuery] string? kategoria,
            [FromQuery] string? jezyk,
            [FromQuery] bool includeInactive,
            CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(typ))
                return BadRequest("Parametr 'typ' jest wymagany.");

            var list = await _mediator.Send(new GetPublicZasobyUIQuery
            {
                Typ = typ,
                Kategoria = kategoria,
                Jezyk = string.IsNullOrWhiteSpace(jezyk) ? "pl-PL" : jezyk,
                IncludeInactive = includeInactive
            }, ct);

            return Ok(list);
        }

        private ProblemDetails BuildProblemDetails(int status, string title, string detail)
        {
            var problem = new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = detail
            };
            problem.Extensions["traceId"] = HttpContext.TraceIdentifier;
            return problem;
        }
    }
}