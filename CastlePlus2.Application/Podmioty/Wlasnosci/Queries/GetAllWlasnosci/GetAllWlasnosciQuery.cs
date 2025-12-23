using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Wlasnosci.Queries.GetAllWlasnosci
{
    public class GetAllWlasnosciQuery : IRequest<List<WlasnoscDto>>
    {
    }
}