using CastlePlus2.Contracts.DTOs.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.Zgloszenia.Queries.GetZglosUsterkeContext
{
    public sealed record GetZglosUsterkeContextQuery : IRequest<ZglosUsterkeContextDto>;
}