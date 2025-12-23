using CastlePlus2.Application.Finanse.Faktury.Commands.CreateFaktura;
using CastlePlus2.Application.Finanse.Faktury.Commands.DeleteFaktura;
using CastlePlus2.Application.Finanse.Faktury.Commands.UpdateFaktura;
using CastlePlus2.Application.Finanse.Faktury.Queries.GetAllFaktury;
using CastlePlus2.Application.Finanse.Faktury.Queries.GetFakturaById;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Contracts.Requests.Finanse;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Finanse
{
    [ApiController]
    [Route("api/finanse/[controller]")]
    public class FakturyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FakturyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<FakturaDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllFakturyQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(FakturaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetFakturaByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(FakturaDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateFakturaRequest request, CancellationToken ct)
        {
            var command = new CreateFakturaCommand
            {
                NumerFaktury = request.NumerFaktury,
                IdPodmiotu = request.IdPodmiotu,
                DataWystawienia = request.DataWystawienia,
                DataSprzedazy = request.DataSprzedazy,
                KodWaluty = request.KodWaluty,
                KwotaNetto = request.KwotaNetto,
                KwotaBrutto = request.KwotaBrutto
            };

            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdFaktury }, result);
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(FakturaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateFakturaRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new UpdateFakturaCommand(id, request), ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeleteFakturaCommand(id), ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
