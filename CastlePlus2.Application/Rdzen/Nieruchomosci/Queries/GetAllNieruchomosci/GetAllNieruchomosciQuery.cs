using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Nieruchomosci.Queries.GetAllNieruchomosci
{
    public sealed record GetAllNieruchomosciQuery : IRequest<List<NieruchomoscDto>>;
}
