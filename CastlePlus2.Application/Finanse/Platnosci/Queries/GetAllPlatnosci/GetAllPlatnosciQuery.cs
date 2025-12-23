using System.Collections.Generic;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.Platnosci.Queries.GetAllPlatnosci
{
    public class GetAllPlatnosciQuery : IRequest<List<PlatnoscDto>>
    {
    }
}
