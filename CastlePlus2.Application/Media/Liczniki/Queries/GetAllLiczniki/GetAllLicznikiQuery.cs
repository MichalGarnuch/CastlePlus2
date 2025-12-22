using System.Collections.Generic;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Liczniki.Queries.GetAllLiczniki
{
    public class GetAllLicznikiQuery : IRequest<List<LicznikDto>>
    {
    }
}
