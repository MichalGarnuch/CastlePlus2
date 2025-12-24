using CastlePlus2.Application.Najem.SkladnikiCzynszu.Commands.CreateSkladnikCzynszu;
using CastlePlus2.Application.Najem.SkladnikiCzynszu.Commands.DeleteSkladnikCzynszu;
using CastlePlus2.Application.Najem.SkladnikiCzynszu.Commands.UpdateSkladnikCzynszu;
using CastlePlus2.Application.Najem.SkladnikiCzynszu.Queries.GetAllSkladnikiCzynszu;
using CastlePlus2.Application.Najem.SkladnikiCzynszu.Queries.GetSkladnikCzynszuById;
using CastlePlus2.Contracts.Requests.Najem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Najem
{
    [ApiController]
    [Route("api/najem/[controller]")]
    public class SkladnikiCzynszuController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SkladnikiCzynszuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSkladnikCzynszuRequest request, CancellationToken ct)
        {
            var command = new CreateSkladnikCzynszuCommand
            {
                IdUmowyNajmu = request.IdUmowyNajmu,
                Nazwa = request.Nazwa,
                KodJednostki = request.KodJednostki,
                Stawka = request.Stawka,
                IloscBazowa = request.IloscBazowa,
                KodIndeksacji = request.KodIndeksacji,
                OdDnia = request.OdDnia,
                DoDnia = request.DoDnia
            };

            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.IdSkladnikaCzynszu }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllSkladnikiCzynszuQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetSkladnikCzynszuByIdQuery(id), ct);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateSkladnikCzynszuRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(new UpdateSkladnikCzynszuCommand(id, request), ct);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeleteSkladnikCzynszuCommand(id), ct);
            return ok ? NoContent() : NotFound();
        }
    }
}