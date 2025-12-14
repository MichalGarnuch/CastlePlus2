using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Wlasnosci.Queries.GetWlasnoscById
{
    public class GetWlasnoscByIdQuery : IRequest<WlasnoscDto?>
    {
        public long IdWlasnosci { get; }

        public GetWlasnoscByIdQuery(long idWlasnosci)
        {
            IdWlasnosci = idWlasnosci;
        }
    }
}
