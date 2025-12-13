using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.Faktury.Queries.GetFakturaById
{
    public class GetFakturaByIdQuery : IRequest<FakturaDto?>
    {
        public long IdFaktury { get; }

        public GetFakturaByIdQuery(long idFaktury)
        {
            IdFaktury = idFaktury;
        }
    }
}
