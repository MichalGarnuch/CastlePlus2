using System.Collections.Generic;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Odczyty.Queries.GetAllOdczyty
{
    public record GetAllOdczytyQuery() : IRequest<List<OdczytDto>>;
}
