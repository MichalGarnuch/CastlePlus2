using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Adresy.Queries.GetAllAdresy
{
    public record GetAllAdresyQuery() : IRequest<List<AdresDto>>;
}
