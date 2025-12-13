using CastlePlus2.Contracts.DTOs.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.ZleceniaPracy.Queries.GetZleceniePracyById
{
    public class GetZleceniePracyByIdQuery : IRequest<ZleceniePracyDto?>
    {
        public long IdZlecenia { get; }

        public GetZleceniePracyByIdQuery(long idZlecenia)
        {
            IdZlecenia = idZlecenia;
        }
    }
}
