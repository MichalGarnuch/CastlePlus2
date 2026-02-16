using CastlePlus2.Application.Finanse.ProcesyFaktury.Commands.GenerateFakturaWydruk;
using CastlePlus2.Application.Finanse.ProcesyFaktury.Commands.GenerateNajemFaktury;
using CastlePlus2.Application.Finanse.ProcesyFaktury.Commands.WystawFakture;
using CastlePlus2.Application.Finanse.ProcesyFaktury.Queries.GetFakturaWydrukTemplates;
using CastlePlus2.Application.Finanse.ProcesyFaktury.Queries.GetWystawFaktureContext;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;
using CastlePlus2.Shared.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CastlePlus2.Api.Controllers.Finanse
{
    [ApiController]
    [Authorize]
    [Route("api/finanse/procesy/faktury")]
    public class ProcesyFakturyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;

        public ProcesyFakturyController(IMediator mediator, IMemoryCache cache)
        {
            _mediator = mediator;
            _cache = cache;
        }

        [HttpGet("context")]
        [ProducesResponseType(typeof(WystawFaktureContextDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContext(CancellationToken ct)
        {
            var context = await _mediator.Send(new GetWystawFaktureContextQuery(), ct);
            return Ok(context);
        }

        [HttpGet("wydruk/templates")]
        [Authorize(Roles = RoleCodes.AdminOrEmployee)]
        [ProducesResponseType(typeof(List<FakturaWydrukTemplateDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWydrukTemplates(CancellationToken ct)
        {
            var templates = await _mediator.Send(new GetFakturaWydrukTemplatesQuery(), ct);
            return Ok(templates);
        }

        [HttpPost("wydruk")]
        [Authorize(Roles = RoleCodes.AdminOrEmployee)]
        [ProducesResponseType(typeof(GenerateFakturaWydrukResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GenerateWydruk([FromBody] GenerateFakturaWydrukRequest request, CancellationToken ct)
        {
            var generated = await _mediator.Send(new GenerateFakturaWydrukCommand
            {
                IdFaktury = request.IdFaktury,
                TemplateDokumentId = request.TemplateDokumentId,
                Format = request.Format,
                IncludeAllocations = request.IncludeAllocations
            }, ct);

            var downloadId = Guid.NewGuid().ToString("N");
            var expiresAt = DateTime.UtcNow.AddMinutes(10);

            _cache.Set(
                $"faktura-wydruk:{downloadId}",
                (generated.Bytes, generated.FileName, generated.ContentType),
                expiresAt);

            return Ok(new GenerateFakturaWydrukResponse
            {
                DownloadUrl = $"/api/finanse/procesy/faktury/wydruk/{downloadId}",
                ExpiresAtUtc = expiresAt,
                Warnings = generated.Warnings
            });
        }

        [HttpGet("wydruk/{id}")]
        [AllowAnonymous]
        public IActionResult DownloadWydruk([FromRoute] string id)
        {
            if (!_cache.TryGetValue($"faktura-wydruk:{id}", out (byte[] Bytes, string FileName, string ContentType) entry))
                return NotFound();

            Response.Headers["Cache-Control"] = "no-store";
            return File(entry.Bytes, entry.ContentType, entry.FileName);
        }

        [HttpPost("wystaw")]
        [ProducesResponseType(typeof(WystawFaktureResultDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Wystaw([FromBody] WystawFaktureRequest request, CancellationToken ct)
        {
            var pozycje = request.Pozycje ?? new List<WystawFakturePozycjaRequest>();

            var result = await _mediator.Send(new WystawFaktureCommand
            {
                NumerFaktury = request.NumerFaktury,
                IdPodmiotu = request.IdPodmiotu,
                DataWystawienia = request.DataWystawienia,
                DataSprzedazy = request.DataSprzedazy,
                KodWaluty = request.KodWaluty,
                Pozycje = pozycje.Select(pozycja => new WystawFakturePozycjaCommand
                {
                    IdKategoriiKosztu = pozycja.IdKategoriiKosztu,
                    Opis = pozycja.Opis,
                    KwotaNetto = pozycja.KwotaNetto,
                    KwotaBrutto = pozycja.KwotaBrutto,
                    Alokacje = pozycja.Alokacje.Select(alokacja => new WystawFaktureAlokacjaCommand
                    {
                        IdEncji = alokacja.IdEncji,
                        KwotaNetto = alokacja.KwotaNetto,
                        KwotaBrutto = alokacja.KwotaBrutto
                    }).ToList()
                }).ToList()
            }, ct);

            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPost("najem/generate")]
        [Authorize(Roles = RoleCodes.AdminOrEmployee)]
        [ProducesResponseType(typeof(GenerateNajemFakturyResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GenerateNajem([FromBody] GenerateNajemFakturyRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new GenerateNajemFakturyCommand
            {
                Miesiac = request.Miesiac,
                DataWystawienia = request.DataWystawienia
            }, ct);

            return Ok(result);
        }
    }
}