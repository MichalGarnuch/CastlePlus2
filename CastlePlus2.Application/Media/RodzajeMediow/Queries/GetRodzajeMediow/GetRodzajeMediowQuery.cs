using System.Collections.Generic;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.RodzajeMediow.Queries.GetRodzajeMediow
{
    public sealed class GetRodzajeMediowQuery : IRequest<List<RodzajMediumDto>>
    {
    }
}
