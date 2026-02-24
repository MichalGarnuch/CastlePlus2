using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Common.Exceptions;
using CastlePlus2.Application.Media.Odczyty.Commands.CreateOdczyt;
using CastlePlus2.Application.Media.Odczyty.Commands.DeleteOdczyt;
using CastlePlus2.Application.Media.Odczyty.Commands.UpdateOdczyt;
using CastlePlus2.Application.Media.Odczyty.Queries.GetAllOdczyty;
using CastlePlus2.Application.Media.Odczyty.Queries.GetOdczytById;
using CastlePlus2.Application.Media.Odczyty.Queries.GetOdczytContext;
using CastlePlus2.Contracts.DTOs.Media;
using CastlePlus2.Contracts.Requests.Media;
using CastlePlus2.Shared.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastlePlus2.Api.Controllers.Media
{
    [ApiController]
    [Authorize]
    [Route("api/media/[controller]")]
    public class OdczytyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OdczytyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Formularz (self-service): Admin/Employee/User
        [HttpGet("context")]
        [Authorize(Roles = RoleCodes.AdminOrEmployeeOrUser)]
        [ProducesResponseType(typeof(OdczytContextDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContext(CancellationToken ct)
        {
            var context = await _mediator.Send(new GetOdczytContextQuery(), ct);
            return Ok(context);
        }

        // Lista (biuro / podgląd): Admin/Employee/Manager
        [HttpGet]
        [Authorize(Roles = RoleCodes.AdminOrManagerOrEmployee)]
        [ProducesResponseType(typeof(List<OdczytDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var list = await _mediator.Send(new GetAllOdczytyQuery(), ct);
            return Ok(list);
        }

        [HttpGet("{id:long}")]
        [Authorize(Roles = RoleCodes.AdminOrManagerOrEmployee)]
        [ProducesResponseType(typeof(OdczytDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetOdczytByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        // Self-service dodawania: Admin/Employee/User
        [HttpPost]
        [Authorize(Roles = RoleCodes.AdminOrEmployeeOrUser)]
        [ProducesResponseType(typeof(OdczytDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] CreateOdczytRequest request, CancellationToken ct)
        {
            var cmd = new CreateOdczytCommand
            {
                IdLicznika = request.IdLicznika,
                DataOdczytu = request.DataOdczytu,
                Wskazanie = request.Wskazanie,
                Zrodlo = request.Zrodlo
            };

            try
            {
                var dto = await _mediator.Send(cmd, ct);
                return CreatedAtAction(nameof(GetById), new { id = dto.IdOdczytu }, dto);
            }
            catch (BusinessConflictException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        // Edycja/usuwanie tylko biuro: Admin/Employee
        [HttpPut("{id:long}")]
        [Authorize(Roles = RoleCodes.AdminOrEmployee)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateOdczytRequest request, CancellationToken ct)
        {
            var ok = await _mediator.Send(new UpdateOdczytCommand
            {
                IdOdczytu = id,
                IdLicznika = request.IdLicznika,
                DataOdczytu = request.DataOdczytu,
                Wskazanie = request.Wskazanie,
                Zrodlo = request.Zrodlo
            }, ct);

            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id:long}")]
        [Authorize(Roles = RoleCodes.AdminOrEmployee)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] long id, CancellationToken ct)
        {
            var ok = await _mediator.Send(new DeleteOdczytCommand(id), ct);
            return ok ? NoContent() : NotFound();
        }
    }
}