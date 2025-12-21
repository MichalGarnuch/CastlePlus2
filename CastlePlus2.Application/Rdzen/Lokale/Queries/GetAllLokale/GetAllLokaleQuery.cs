using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Lokale.Queries.GetAllLokale
{
    public sealed record GetAllLokaleQuery : IRequest<List<LokalDto>>;
}
